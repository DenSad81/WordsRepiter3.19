using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public Word(int id, string wordEn, string transcryption, string wordRu, int dict_id, int correctAnswers)
    {
        Id = id;
        WordEn = wordEn;
        Transcryption = transcryption;
        WordRu = wordRu;
        Dict_id = dict_id;
        CorrectAnswers = correctAnswers;
    }

    public int Id { get; set; }
    public string WordEn { get; set; }
    public string Transcryption { get; set; }
    public string WordRu { get; set; }
    public int Dict_id { get; set; }
    public int CorrectAnswers { get; set; }

    public void ShowData() =>
         Debug.Log($"{Id} {WordEn} {Transcryption} {WordRu} {Dict_id} {CorrectAnswers}");
}
