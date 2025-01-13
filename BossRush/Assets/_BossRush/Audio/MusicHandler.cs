using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private MusicSettings musicSettings; // Reference to the settings ScriptableObject
    [SerializeField] private AudioSource audioSource; // The AudioSource to play music
    [SerializeField] private float fadeDuration = 2f; // Duration of fade in/out

    private List<MusicSettings.Song> unplayedSongs = new List<MusicSettings.Song>();
    private List<MusicSettings.Song> playedSongs = new List<MusicSettings.Song>();

    private Coroutine fadeCoroutine;

    /// <summary>
    /// Initialize the handler by loading all songs from the settings.
    /// </summary>
    public void Initialize()
    {
        if (musicSettings == null)
        {
            Debug.LogError("MusicSettings is not assigned!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        unplayedSongs = new List<MusicSettings.Song>(musicSettings.AllSongs);
        playedSongs.Clear();
    }

    /// <summary>
    /// Play a random song by mood with fade-in/out.
    /// </summary>
    /// <param name="mood">The mood of the song to select.</param>
    public void PlaySongByMood(MusicSettings.Mood mood)
    {
        var song = GetRandomSongByMood(mood);
        if (song != null)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(PlaySongWithFade(song));
        }
    }

    /// <summary>
    /// Get a random song based on the specified mood.
    /// </summary>
    /// <param name="mood">The mood of the song to select.</param>
    /// <returns>A random song matching the mood, or null if none are available.</returns>
    private MusicSettings.Song GetRandomSongByMood(MusicSettings.Mood mood)
    {
        List<MusicSettings.Song> songsByMood = unplayedSongs.FindAll(song => song.mood == mood);

        if (songsByMood.Count == 0)
        {
            ResetSongsByMood(mood);
            songsByMood = unplayedSongs.FindAll(song => song.mood == mood);
        }

        if (songsByMood.Count > 0)
        {
            MusicSettings.Song selectedSong = songsByMood[Random.Range(0, songsByMood.Count)];
            MoveSongToPlayed(selectedSong);
            return selectedSong;
        }

        Debug.LogWarning($"No songs available for the selected mood: {mood}");
        return null;
    }

    /// <summary>
    /// Coroutine to handle fade-out, change song, and fade-in.
    /// </summary>
    private IEnumerator PlaySongWithFade(MusicSettings.Song song)
    {
        // Fade out the current song
        yield return StartCoroutine(FadeOut());

        // Change the song
        audioSource.clip = song.clip;
        audioSource.Play();

        // Fade in the new song
        yield return StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Fade out the current song.
    /// </summary>
    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume for future use
    }

    /// <summary>
    /// Fade in the new song.
    /// </summary>
    private IEnumerator FadeIn()
    {
        audioSource.volume = 0f;
        float targetVolume = 1f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    /// <summary>
    /// Reset songs of a specific mood from played to unplayed.
    /// </summary>
    /// <param name="mood">The mood to reset songs for.</param>
    private void ResetSongsByMood(MusicSettings.Mood mood)
    {
        List<MusicSettings.Song> songsToReset = playedSongs.FindAll(song => song.mood == mood);
        playedSongs.RemoveAll(song => song.mood == mood);
        unplayedSongs.AddRange(songsToReset);
    }

    /// <summary>
    /// Reset all songs, moving them back to the unplayed list.
    /// </summary>
    public void ResetAllSongs()
    {
        unplayedSongs.AddRange(playedSongs);
        playedSongs.Clear();
    }

    /// <summary>
    /// Move a song from unplayed to played.
    /// </summary>
    /// <param name="song">The song to move.</param>
    private void MoveSongToPlayed(MusicSettings.Song song)
    {
        unplayedSongs.Remove(song);
        playedSongs.Add(song);
    }
}
