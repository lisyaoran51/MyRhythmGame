﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Base.Sheetmusics.IO {
    public class FileReader : IDisposable{
        private readonly string path;

        public FileReader(string path) {
            this.path = path;
        }

        public Stream GetStream(string name) {
            return File.OpenRead(Path.Combine(path, name));
        }

        /// <summary>
        /// 提供path路徑下的所有檔案位址
        /// </summary>
        public IEnumerable<string> Filenames {
            private set { }
            get {
                return Directory.GetFiles(path, "*", SearchOption.AllDirectories).Select(p => {
                    p = p.Replace(Path.DirectorySeparatorChar, '/').TrimEnd('/');
                    string folder = path.Replace(Path.DirectorySeparatorChar, '/').TrimEnd('/');
                    
                    return p.Substring(folder.Length + 1);
                }).ToArray();
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~FileReader() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose() {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}