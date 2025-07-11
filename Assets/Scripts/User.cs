//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

public class User
{
    public User(int id, string name, int amountToMem, int showTranscryption, int errorPenalty, string direction, int isActive, int currentDictId, int autoPronance, string modeChange, int quantityRepit)
    {
        Id = id;
        Name = name;
        AmountToMem = amountToMem;
        ShowTranscryption = showTranscryption;
        ErrorPenalty = errorPenalty;
        Direction = direction;
        IsActive = isActive;
        CurrentDictId = currentDictId;
        AutoPronounce = autoPronance;
        ModeChange = modeChange;
        QuantityRepit = QuantityRepit;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int AmountToMem { get; set; }
    public int ShowTranscryption { get; set; }
    public int ErrorPenalty { get; set; }
    public string Direction { get; set; }
    public int IsActive { get; set; }
    public int CurrentDictId { get; set; }
    public int AutoPronounce { get; set; }
    public string ModeChange { get; set; }
    public int QuantityRepit { get; set; }
}

