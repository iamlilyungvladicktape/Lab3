using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

class Set
{
    private List<int> elements;

    public Set()
    {
        elements = new List<int>();

    }

    public void Add(int element)
    {
        if (!elements.Contains(element))
        {
            elements.Add(element);
        }
    }

    public void Remove(int element)
    {
        elements.Remove(element);
    }
    public bool Containts(int element)
    {
        return elements.Contains(element);
    }

    public bool IsSubsetof(Set set1)
    {
        foreach (int element in elements)
        {
            if (!set1.Containts(element))
            {
                return false;
            }
        }
        return true;
    }

    public bool Equals(Set set1)
    {
        if (elements.Count != set1.elements.Count)
        {
            return false;
        }
        foreach (int element in elements)
        {
            if (!set1.Containts(element))
            {
                return false;
            }
            
        }
        return true;
    }   

    public Set Union(Set set1)
    {
        Set result = new Set();
        result.elements.AddRange(elements);
        
        foreach(int element in set1.elements)
        {
            if (!result.Containts(element))
            {
                result.Add(element);
            }
        }
        return result;
    }
    public Set Intersection(Set set1)
    {
        Set result = new Set();
        foreach(int element in elements)
        {
            if (set1.Containts(element))
            {
                result.Add(element);
            }
        }
        return result;
    }
    public Set Difference(Set set1)
    {
        Set result = new Set();
        foreach (int element in elements)
        {
            if (!set1.Containts(element))
            {
                result.Add(element);
            }
        }
        return result;
    }

    public Set SymmetricDifference(Set set1)
    {
        Set result = new Set();
        foreach(int element in elements)
        {
            if (!set1.Containts(element))
            {
                result.Add(element);
            }
        }
        foreach(int element in set1.elements)
        {
            if (!elements.Contains(element))
            {
                result.Add(element);
            }
        }
        return result;
    }
    public IEnumerable<int> Elements
    {
        get { return elements; }
    }

    static void Main(string[] args)
    {
        Set set1 = new Set();
        set1.Add(1);
        set1.Add(2);
        set1.Add(3);
        set1.Add(4);
        set1.Add(5);

        Set set2 = new Set();
        set2.Add(3);
        set2.Add(4);
        set2.Add(5);
        set2.Add(6);
        set2.Add(7);

        Console.WriteLine("set1: {0}", string.Join(",", set1.Elements));
        Console.WriteLine("set2: {0}", string.Join(",", set2.Elements));

        Console.WriteLine("set1 contains 3: {0}", set1.Containts(3));
        Console.WriteLine("set1 contains 6: {0}", set1.Containts(6));

        Set union = set1.Union(set2);
        Console.WriteLine("union: {0}", string.Join(",", union.Elements));

        Set intersection = set1.Intersection(set2);
        Console.WriteLine("intersection: {0}", string.Join(",", intersection.Elements));

        Set difference = set1.Difference(set2);
        Console.WriteLine("difference: {0}", string.Join(",", difference.Elements));

        Set symmetricDifference = set1.SymmetricDifference(set2);
        Console.WriteLine("symmetric difference: {0}", string.Join(",", symmetricDifference.Elements));

        Console.WriteLine("set1 is subset of set2: {0}", set1.IsSubsetof(set2));
        Console.WriteLine("set1 is equal to set2: {0}", set1.Equals(set2));
        // Збереження об'єкту у JSON 
        set1.SaveToJSON("set1.json");

        // Створення об'єкту з JSON файлу
        Set setFromFile = Set.LoadFromJSONFile("set1.json");

    }
    public void SaveToJSON(string filename)
    {
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(filename, json);
    }
    public static Set LoadFromJSONFile(string filename)
    {
        string json = File.ReadAllText(filename);
        Set set = JsonSerializer.Deserialize<Set>(json);
        return set;
    }

}

