using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ColorThemeManager : MonoBehaviour
{
    [System.Serializable]
    public struct LevelTheme
    {
        [Range(0f, 1f)] public float player_hue;
        [Range(0f, 1f)] public float platform_hue;
        [Range(0f, 1f)] public float background_hue;
    }

    [SerializeField] private GameLoop game_loop_script;

    [Header("Presets")]
    [SerializeField] private List<LevelTheme> level_themes = new();
    [SerializeField] private float transition_duration = 1f;

    [Header("Target Materials")]
    [SerializeField] private Material player_material;
    [SerializeField] private Material platform_material;
    [SerializeField] private Material particle_material;
    [SerializeField] private Material background_material;

    private static readonly int hue_property = Shader.PropertyToID("_Hue");
    private Coroutine active_transition;

    private void Awake()
    {
        ChangeLevelThemeInstant(0);
    }

    public void ChangeLevelThemeInstant(int override_level = -1)
    {
        if (level_themes.Count == 0) return;
        LevelTheme target_theme = level_themes[Mathf.Clamp(
            (override_level != -1 ? override_level : game_loop_script.level) 
            % level_themes.Count, 0, level_themes.Count - 1)];
        player_material.SetFloat(hue_property, target_theme.player_hue);
        platform_material.SetFloat(hue_property, target_theme.platform_hue);
        particle_material.SetFloat(hue_property, target_theme.platform_hue + 0.02f);
        background_material.SetFloat(hue_property, target_theme.background_hue);
    }

    public void ChangeLevelTheme()
    {
        if (level_themes.Count == 0) return;
        LevelTheme target_theme = level_themes[Mathf.Clamp(game_loop_script.level
            % level_themes.Count, 0, level_themes.Count - 1)];
        if (active_transition != null) StopCoroutine(active_transition);
        active_transition = StartCoroutine(TransitionToTheme(target_theme));
    }

    private IEnumerator TransitionToTheme(LevelTheme target)
    {
        float elapsed = 0f;

        float start_player = player_material.GetFloat(hue_property);
        float start_platform = platform_material.GetFloat(hue_property);
        float start_bg = background_material.GetFloat(hue_property);

        while (elapsed < transition_duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / transition_duration);

            player_material.SetFloat(hue_property, LerpHueShortestPath(start_player, target.player_hue, progress));
            platform_material.SetFloat(hue_property, LerpHueShortestPath(start_platform, target.platform_hue, progress));
            particle_material.SetFloat(hue_property, LerpHueShortestPath(start_platform, target.platform_hue, progress) + 0.02f);
            background_material.SetFloat(hue_property, LerpHueShortestPath(start_bg, target.background_hue, progress));

            yield return null;
        }

        player_material.SetFloat(hue_property, target.player_hue);
        platform_material.SetFloat(hue_property, target.platform_hue);
        particle_material.SetFloat(hue_property, target.platform_hue + 0.02f);
        background_material.SetFloat(hue_property, target.background_hue);
    }

    private float LerpHueShortestPath(float start, float end, float t)
    {
        float normalized = (Mathf.LerpAngle(start * 360f, end * 360f, t) / 360f) % 1.0f;
        return normalized < 0f ? normalized + 1.0f : normalized;
    }

    private void OnDestroy()
    {
        ChangeLevelThemeInstant(0);
    }
}