using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var wordsSet = new HashSet<string>(words);
        var pairSet = new HashSet<string>();

        foreach (string word in words)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            if (wordsSet.Contains(new string(charArray)) && Array.IndexOf(words, word) > Array.IndexOf(words, new string(charArray)))
            {
                string pair = $"{word} & {new string(charArray)}";
                pairSet.Add(pair);
            }
        }

        string[] returnArray = new string[pairSet.Count];
        pairSet.CopyTo(returnArray);

        return returnArray;
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (!degrees.ContainsKey(fields[3]))
            {
                degrees[fields[3]] = 1;

            }
            else
            {
                degrees[fields[3]]++;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        string cleanWord1 = word1.ToLower().Replace(" ", "");
        string cleanWord2 = word2.ToLower().Replace(" ", "");
        var word1Map = new Dictionary<char, int>();
        var word2Map = new Dictionary<char, int>();
        int isSameCount = 0;

        if (cleanWord1.Length != cleanWord2.Length)
        {
            return false;
        }

        for (int i = 0; i < cleanWord1.Length; i++)
        {
            if (word1Map.ContainsKey(cleanWord1[i]))
            {
                word1Map[cleanWord1[i]]++;
            }
            else
            {
                word1Map[cleanWord1[i]] = 1;
            }

            if (word2Map.ContainsKey(cleanWord2[i]))
            {
                word2Map[cleanWord2[i]]++;
            }
            else
            {
                word2Map[cleanWord2[i]] = 1;
            }
        }

        foreach (var key in word1Map.Keys)
        {
            if (word2Map.ContainsKey(key) && word1Map[key] == word2Map[key])
            {
                isSameCount++;
            }
        }

        return isSameCount == word1Map.Count;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var currentDayEntries = new List<string>();

        foreach (var entry in featureCollection.Features)
        {
            DateTime entryDate = DateTimeOffset.FromUnixTimeMilliseconds(entry.Properties.Time).UtcDateTime;
            DateTime currentDate = DateTime.Now;
            string formattedEntryDate = entryDate.ToString("dd/MM/yyyy");
            string formattedCurrentDate = currentDate.ToString("dd/MM/yyyy");

            if (formattedCurrentDate.Equals(formattedEntryDate))
            {
                string displayEntry = $"{entry.Properties.Place} - Mag {entry.Properties.Mag}";
                currentDayEntries.Add(displayEntry);
            }

        }

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magnitude.
        // 3. Return an array of these string descriptions.
        return currentDayEntries.ToArray();
    }
}