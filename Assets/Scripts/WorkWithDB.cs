using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEditor.SearchService;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.Assertions;

public class WorkWithDB : MonoBehaviour
{
    private DB db = new DB("mainDB.bytes");

    private void Start()
    {
        //List<Word> words = GetAllWordsFromDB();

        ////foreach (var word in words)
        ////    word.ShowData();

        //Debug.Log("#######################");

        //GetWordFromDB(1960).ShowData();

        //IncreaseCorrectAnswersInTableWords(1960);

        //GetWordFromDB(1960).ShowData();

        //DeleteWordIntoDB(1971);

        //SetCorrectAnswersInTableWords(1960,333);
        //GetWordFromDB(1960).ShowData();
        //SetAllCorrectAnswersInTableWords(3);
        //GetWordFromDB(1960).ShowData();

    }

    public void IncreaseCorrectAnswersInTableWords(int idWord)
    {
        string temp = db.ExecuteQueryWithAnswer($"SELECT correctAnswers FROM Words WHERE id={idWord};");
        int t = int.Parse(temp);
        t++;
        db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers={t} WHERE id={idWord};");
    }

    public void DecreaseCorrectAnswersInTableWords(int idWord)
    {
        string temp = db.ExecuteQueryWithAnswer($"SELECT correctAnswers FROM Words WHERE id={idWord};");
        int value = int.Parse(temp);

        if (value > 0)
            value--;

        db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers = {value} WHERE id={idWord};");
    }

    public void SetCorrectAnswersInTableWords(int idWord, int quantityCorrectAnswers=1)
    {
        db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers={quantityCorrectAnswers} WHERE id={idWord};");
    }

    public void SetAllCorrectAnswersInTableWords( int quantityCorrectAnswers = 1)
    {
        db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers={quantityCorrectAnswers};");
    }

    public List<Word> GetAllWordsFromDB()
    {
        DataTable table = db.ExecuteQueryWithAnswerAsDataTable("SELECT* FROM Words;");
        // Debug.Log(table.Rows.Count);
        // Debug.Log(table.Columns.Count);
        List<Word> words = new List<Word>();

        for (int i = 0; i < table.Rows.Count; i++)
            words.Add(new Word(int.Parse(table.Rows[i][0].ToString()),
                                         table.Rows[i][1].ToString(),
                                         table.Rows[i][2].ToString(),
                                         table.Rows[i][3].ToString(),
                               int.Parse(table.Rows[i][4].ToString()),
                               int.Parse(table.Rows[i][5].ToString())));
        return words;
    }

    public Word GetWordFromDB(int idWord = 0)
    {
        DataTable table = db.ExecuteQueryWithAnswerAsDataTable($"SELECT* FROM Words WHERE id={idWord};");
        //table - содержит всего одну строку
        // Debug.Log(table.Rows.Count);
        // Debug.Log(table.Columns.Count);
        if (table.Rows.Count != 1)
            return null;

        return new Word(int.Parse(table.Rows[0][0].ToString()),
                                  table.Rows[0][1].ToString(),
                                  table.Rows[0][2].ToString(),
                                  table.Rows[0][3].ToString(),
                        int.Parse(table.Rows[0][4].ToString()),
                        int.Parse(table.Rows[0][5].ToString()));
    }

    public void AddWordWithIdToDB(Word word)
    {
        if (db.ExecuteQueryWithAnswer($"SELECT id FROM Words WHERE id={word.Id};") != null)
            throw new InvalidOperationException("This index is bisy");

        db.ExecuteQueryWithoutAnswer($"INSERT INTO Words VALUES({word.Id}, '{word.WordEn}', '{word.Transcryption}', '{word.WordRu}', {word.Dict_id}, {word.CorrectAnswers});");
    }

    public void AddWordWithoutIdToDB(Word word)
    {
        db.ExecuteQueryWithoutAnswer($"INSERT INTO Words (wordEn, transcryption, wordRu, dict_id, correctAnswers)" +
                   $"VALUES('{word.WordEn}', '{word.Transcryption}', '{word.WordRu}', {word.Dict_id}, {word.CorrectAnswers});");
    }

    public void DeleteWordIntoDB(int idWord)
    {
        db.ExecuteQueryWithoutAnswer($"DELETE FROM Words WHERE id={idWord};");
    }

}