using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

//Needs the CSVReader.cs inside the project in order to work
//Needs an appropiate DataItem for your values as well

//Converts any .csv to a DataItem List
//Output options for string, int/float, boolean, Color and Lists of strings or int/floats
//(Vectors should be easy to convert from List<float>)

//Is specialized on the .csv spreadsheet exports from the Project Manager by Maurice Oelze
[HelpURL("https://berlinnights.itch.io/project-manager")]
public class CSVDataImport : MonoBehaviour
{
    [Header("Input Options")]
    public string loadFile = "TestFile";
    public char listSeparator = '#';
    //Is true when the second row has the column type info
    public bool typesIncluded = true;
    [Header("Output")]
    public List<string> keys;
    public List<string> types;
    //Define an appropiate Item to hold your values
    public List<DataItem> data = new List<DataItem>();
    
    public DataItem blankItem;
    
    void Start()
    {
        dataRead();
    }
    
    public void dataRead()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(loadFile);
        foreach(string key in data[0].Keys)
        {
            keys.Add(key);
        }
        if (typesIncluded == true)
        {
            foreach (string key in data[0].Values)
            {
                types.Add(key);
            }
           
            for (int i = 1; i < data.Count; i++)
            {
                //Dictionary<string, object> dic = data[i];

                int ID = convert_StringToInt(data[i][keys[0]].ToString());
                string var1 = data[i][keys[1]].ToString();
                List<string> var2 = convert_StringToList(data[i][keys[2]].ToString());
                bool var3 = convert_StringToBool(data[i][keys[3]].ToString());
                Color var4 = convert_StringToColor(data[i][keys[4]].ToString());
                AddItem(ID, var1, var2, var3, var4);
            }
            
        }
        else
        {
            foreach(string key in keys)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    Dictionary<string, object> dic = data[i];

                    int ID = convert_StringToInt(data[i][keys[0]].ToString());
                    string var1 = data[i][keys[1]].ToString();
                    List<string> var2 = convert_StringToList(data[i][keys[2]].ToString());
                    bool var3 = convert_StringToBool(data[i][keys[3]].ToString());
                    Color var4 = convert_StringToColor(data[i][keys[4]].ToString());
                    AddItem(ID, var1, var2, var3, var4);
                }
            }
            
        }
    }
    
    private int convert_StringToInt(string _string)
    {
        int i = int.Parse(_string, System.Globalization.NumberStyles.Integer);
        return i;
    }

    private float convert_StringToFloat(string _string)
    {
        float i = float.Parse(_string, CultureInfo.InvariantCulture);
        return i;
    }

    private bool convert_StringToBool(string _string)
    {
        if (_string == "True" | _string == "true" | _string == "1")
        {
            return true;

        }
        return false;
    }

    private Color convert_StringToColor(string _string)
    {
        Color color = new Color();
        string[] _split = _string.Split(listSeparator);
        float[] _colorArray = new float[4];
        int i = 0;
        foreach(string _s in _split)
        {
            float _channel = float.Parse(_s, CultureInfo.InvariantCulture);
            //float _channel =  float.Parse(_s);
            //float _channel = _s.ConvertTo(float);
            _colorArray[i] = _channel;
            i += 1;
        }
        color.r = _colorArray[0];
        color.g = _colorArray[1];
        color.b = _colorArray[2];
        color.a = _colorArray[3];
        return color;
    }

    private List<string> convert_StringToList(string _string)
    {
        List<string> _list = new List<string>();
        string[] _array = _string.Split(listSeparator);
        foreach (string _s in _array)
        {
            _list.Add(_s);
        }
        return _list;
    }

    void AddItem(int ID, string var1, List<string> var2, bool var3, Color var4)
    {
        DataItem tempItem = new DataItem(blankItem);

        tempItem.var1 = var1;
        tempItem.var2 = var2;
        tempItem.var3 = var3;
        tempItem.var4 = var4;
  

        data.Add(tempItem);
    }
}
