using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicSettings", menuName = "Boss Rush/MusicSettings")]
public class MusicSettings : ScriptableObject
{
    [System.Serializable]
    public class Song
    {
        public string name; // Display name of the song
        public AudioClip clip; // Audio file for the song
        public Mood mood; // Mood category of the song
    }

    public enum Mood
    {
        Happy,       // For cheerful and upbeat songs
        Sad,         // For emotional and slower songs
        Calm,        // For relaxing and tranquil songs
        Energetic    // For fast-paced and exciting songs
    }

    [SerializeField]
    private List<Song> allSongs = new List<Song>();

    /// <summary>
    /// Provides read-only access to the list of all songs.
    /// </summary>
    public List<Song> AllSongs => allSongs;
}
