﻿#pragma warning disable 0626
#pragma warning disable 0649

using System;
using UnityEngine;

public class patch_MainMenuFoyerController : MainMenuFoyerController {
    public static patch_MainMenuFoyerController Instance = null;

    private float _orig_height;

    private extern void orig_Awake();
    private void Awake() {
        if (Instance == null) Instance = this;
        orig_Awake();

        _orig_height = VersionLabel.Height;

        VersionLabel.Text = $"Gungeon {VersionLabel.Text}";
        VersionLabel.Color = new Color32(255, 255, 255, 255);
        VersionLabel.Shadow = true;
        VersionLabel.ShadowOffset = new Vector2(1, -1);
        VersionLabel.ShadowColor = new Color32(0, 0, 0, 255);

        for (int i = 0; i < ETGMod.Backend.AllBackends.Count; i++) {
            var backend = ETGMod.Backend.AllBackends[i];
            AddLine($"{backend.Name} {backend.StringVersion}");
        }
    }

    public void AddLine(string line) {
        if (VersionLabel.Text.Length > 0) {
            VersionLabel.Text += $"\n{line}";
        } else {
            VersionLabel.Text = line;
        }

        VersionLabel.Position = new UnityEngine.Vector3(VersionLabel.Position.x, _orig_height - VersionLabel.Height);
    }
}