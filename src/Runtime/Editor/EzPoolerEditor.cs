#if UNITY_EDITOR
using System.IO;
using Racer.EzPooler.Utilities;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace Racer.EzPooler.Editor
{
    internal class EzPoolerEditor : UnityEditor.Editor
    {
        private const string ContextMenuPath = "Racer/EzPooler/";
        private const string SamplesPath = "Assets/Samples/EzPooler";

        private static RemoveRequest _removeRequest;
        private const string PkgId = "com.racer.ezpooler";


        [MenuItem(ContextMenuPath + "Remove Package(recommended)")]
        private static void RemovePackage()
        {
            _removeRequest = Client.Remove(PkgId);
            EditorApplication.update += RemoveRequest;
        }

        private static void RemoveRequest()
        {
            if (!_removeRequest.IsCompleted) return;

            switch (_removeRequest.Status)
            {
                case StatusCode.Success:
                {
                    DirUtils.DeleteDirectory(SamplesPath);
                    AssetDatabase.Refresh();

                    break;
                }
                case >= StatusCode.Failure:
                    EzLogger.Warn($"Failed to remove package: '{PkgId}'\n{_removeRequest.Error.message}");
                    break;
            }

            EditorApplication.update -= RemoveRequest;
        }
    }

    internal static class DirUtils
    {
        public static void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path)) return;

            Directory.Delete(path, true);
            DeleteEmptyMetaFiles(path);
        }

        private static void DeleteEmptyMetaFiles(string directory)
        {
            if (Directory.Exists(directory)) return;

            var metaFile = directory + ".meta";

            if (File.Exists(metaFile))
                File.Delete(metaFile);
        }
    }
#endif
}