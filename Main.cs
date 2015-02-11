using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

class FunnyWords
{

	public string fileName = "alastalon_Salissa.txt";
	private Dictionary<string, int> funnyWords;
	private string vowels = "aeiouyäöäAEIOUYÄÖ";
	private char[] trimChars = {'.',',',':',';','!','?','\"','\''};
	private string[] words;
	private string line;
	private StreamReader streamReader;
	static int highestScore = 0;
	
	public void Play () 
	{
		funnyWords = new Dictionary<string, int>();
		streamReader = new StreamReader (fileName);
		
		while ((line = streamReader.ReadLine()) != null) 
		{
			if(line != string.Empty) 
			{
				words = line.Split();
				foreach(string s in words) 
				{
					string t = s.Trim(trimChars);
					ProcessCurrentWord(t);
				}
			}
		}
		PrintFunniestWords ();
	}

	public Dictionary<string, int> GetFunnyWords() {
		return funnyWords;
	}

	private void PrintFunniestWords() 
	{
		foreach (KeyValuePair<string, int> word in funnyWords) 
		{
			if(word.Value == highestScore) 
			{
				Console.WriteLine(word.Key + " "+word.Value);
			}		
		}
	}
	
	private void ProcessCurrentWord(string s) 
	{
		double count = 0;
		int total = 0;
		for(int i = 0; i<s.Length; i++) 
		{
			if(IsVowel(s[i])) 
			{
				count ++;
			}
			if(!IsVowel(s[i]) || i == s.Length-1) 
			{
				total += (int) (count * Math.Pow (2, count));
				count = 0;
			}
		}
		if(!funnyWords.ContainsKey(s))
		{
			funnyWords.Add (s, total);
			if(total > highestScore) 
			{
				highestScore = total;
			}
		}
	}
	
	private bool IsVowel(char c) 
	{
		foreach(char currentChar in vowels) 
		{
			if(c == currentChar) 
			{
				return true;
			}
		}
		return false;
	}

	public static void Main (string[] args)
	{
		FunnyWords fw = new FunnyWords ();
		fw.Play ();
	}
}

