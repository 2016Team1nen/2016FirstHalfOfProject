using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;    //WAV
using Microsoft.Xna.Framework.Media;    //MP3

namespace Zombie.Device
{
    class Sound
    {
        private ContentManager contentManager;
        private Dictionary<string, Song> bgms;  //MP3管理用
        private Dictionary<string, SoundEffect> soundEffect;    //WAV管理用
        private Dictionary<string, SoundEffectInstance> seInstance;     //WAVインスタンス管理用
        private List<SoundEffectInstance> sePlayList;   //WAVインスタンスの再生リスト

        private string currentBGM;  //現在再生中のアセット名

        public Sound(ContentManager content) {
            contentManager = content;
            MediaPlayer.IsRepeating = true;

            bgms = new Dictionary<string, Song>();
            soundEffect = new Dictionary<string, SoundEffect>();
            seInstance = new Dictionary<string, SoundEffectInstance>();
            sePlayList = new List<SoundEffectInstance>();
            currentBGM = null;
        }


        #region BGM関連
        public void LoadBGM(string name, string filepath = "./") {
            if (bgms.ContainsKey(name)) { return; }
            bgms.Add(name, contentManager.Load<Song>(filepath + name));
        }

        public bool IsStoppedBGM() {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        public bool IsPlayingBGM() {
            return (MediaPlayer.State == MediaState.Playing);
        }

        public bool IsPausedBGM() {
            return (MediaPlayer.State == MediaState.Paused);
        }

        public void StopBGM() {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        public void PlayeBGM(string name) {
            if (currentBGM == name) { return; }
            if (IsPlayingBGM()) {
                StopBGM();
            }
            MediaPlayer.Volume = 0.5f;
            currentBGM = name;
            MediaPlayer.Play(bgms[currentBGM]);
        }

        public void ChangeBGMLoopFlag(bool loopFlag) {
            MediaPlayer.IsRepeating = loopFlag;
        }

        #endregion


        #region WAV関連
        public void LoadSE(string name, string filepath = "./") {
            if (soundEffect.ContainsKey(name)) { return; }
            soundEffect.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        public void CreateSEInstance(string name) {
            if (seInstance.ContainsKey(name)) { return; }
            seInstance.Add(name, soundEffect[name].CreateInstance());
        }

        public void PlaySE(string name){
            soundEffect[name].Play();
        }

        public void PlaySEInstance(string name, bool loopFlag = false) {
            var data = seInstance[name];
            data.Play();
            sePlayList.Add(data);
        }

        public void PausedSE(string name) {
            foreach (var se in sePlayList) {
                if (se.State == SoundState.Playing) {
                    se.Stop();
                }
            }
        }

        public void RemoveSE() {
            sePlayList.RemoveAll(se => se.State == SoundState.Stopped);
        }

        #endregion

        public void Unload() {
            bgms.Clear();
            soundEffect.Clear();
            sePlayList.Clear();
        }

    }
}
