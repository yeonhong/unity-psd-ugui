﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace PSDUnity.UGUI
{
    public class PanelLayerImport : LayerImport
    {
        public PanelLayerImport()
        {
            _suffix = "Panel";
        }
        public override GameObject CreateTemplate()
        {
            return new GameObject("Empty", typeof(RectTransform));
        }

        public override UGUINode DrawLayer(GroupNode layer, UGUINode parent)
        {
            UGUINode node = CreateRootNode(layer.displayName, layer.rect, parent);

            if (layer.children != null)
                ctrl.DrawLayers(layer.children.ConvertAll(x => x as GroupNode).ToArray(), node);//子节点

            Graphic background;

            DrawImages(layer, node, out background);

            TryDrawPanel(background, layer, node);

            return node;

        }

        /// <summary>
        /// 绘制图片
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="node"></param>
        /// <param name="background"></param>
        private void DrawImages(GroupNode layer, UGUINode node, out Graphic background)
        {
            background = null;
            for (int i = 0; i < layer.images.Count; i++)
            {
                ImgNode image = layer.images[i];

                if (MatchAddress(image.Name, rule.backgroundAddress))
                {
                    if (image.type == ImgType.Texture)
                    {
                        background = node.InitComponent<UnityEngine.UI.RawImage>();
                    }
                    else
                    {
                        background = node.InitComponent<UnityEngine.UI.Image>();
                    }

                    if (background)
                    {
                        PSDImporterUtility.SetPictureOrLoadColor(image, background);
                        SetRectTransform(image.rect, background.GetComponent<RectTransform>());
                        background.name = layer.displayName;
                    }
                }
                else
                {
                    ctrl.DrawImage(image, node);
                }
            }
        }

        /// <summary>
        /// 试图为Panel类型的图片添加空背景
        /// </summary>
        /// <param name="background"></param>
        /// <param name="layer"></param>
        /// <param name="node"></param>
        private void TryDrawPanel(Graphic background, GroupNode layer, UGUINode node)
        {
            if (background == null)
            {
                background = node.InitComponent<UnityEngine.UI.Image>();
                SetRectTransform(layer.rect, background.GetComponent<RectTransform>());
                Color color;
                if (ColorUtility.TryParseHtmlString("#FFFFFF01", out color))
                {
                    background.color = color;
                }
                background.name = layer.displayName;
            }
        }
    }
}