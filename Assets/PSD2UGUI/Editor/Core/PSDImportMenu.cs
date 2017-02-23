﻿using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor.SceneManagement;

namespace PSDUIImporter
{
    //------------------------------------------------------------------------------
    // class definition
    //------------------------------------------------------------------------------
    public class PSDImportMenu : Editor
    {

        [MenuItem("Assets/Create/PSDImport ...")]
        static public void ImportHogSceneMenuItem()
        {
            string startPath = null;
            if (Selection.activeObject != null)
            {
                startPath = AssetDatabase.GetAssetPath(Selection.activeObject);
                startPath = Path.GetDirectoryName(startPath);
            }

            string inputFile = EditorUtility.OpenFilePanel("Choose PSDUI File to Import", startPath ?? Application.dataPath, "xml");
            if ((inputFile != null) && (inputFile != "") && (inputFile.StartsWith(Application.dataPath)))
            {
                PSDImportCtrl import = new PSDUIImporter.PSDImportCtrl(inputFile);
                import.BeginDrawUILayers();
                import.BeginSetUIParents();
            }
            GC.Collect();
        }
    }
}