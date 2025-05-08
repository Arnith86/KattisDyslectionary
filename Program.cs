using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//Console.SetIn(new StreamReader("dyslectionary_10_groups_with_edges.txt"));

///<summary>
/// Problem:	Sort groups of words in a dictionary order but instead of n -> n+1 -> n+2..  third letter and so on. 
///				We do it in reverse, starting with the last letter. 
///				
///	Expected input:	Up to 100 groups of words. Each group can have up to 100 words.
///			
///			 Input:	1 word per line.
///					Groups are separated by a black line.
///			
/// 
///	The simple explanation of how i solve this problem is when i register a word i convert the strings into char arrays, 
///	reverse the order of the letters and add it to the word group. This list is then sorted with sortBy. And lastly all words
///	are rebuilt into strings.
/// </summary>


string input;
int wordGroupCounter = 0;
int wordCount = 0;
bool firstGroupPrinted = false;

List<char[]> words = new List<char[]>();

while ((input = Console.ReadLine()) != null)
{
	// catches edge case scenarios,
	if (string.IsNullOrWhiteSpace(input))
	{
		HandleGroup(words);
		wordGroupCounter++;
		wordCount = 0;
	}
	else
	{
		// Converts the word into a char array reverse it and adds it to the current group
		// Remove any excessive whitespace inputs.
		AddWordsToCharArrayList(input.Trim());
		wordCount++;
	}
}

// handles the final group if there are any
if (wordCount > 0)
{
	HandleGroup(words);
	wordCount = 0;
	firstGroupPrinted = false;
}

// Reverses and adds the inputted word to the word group list 
void AddWordsToCharArrayList(string input)
{
	char[] wordCharArray = input.ToCharArray();
	Array.Reverse(wordCharArray);
	words.Add(wordCharArray);
}

void HandleGroup(List<char[]> wordGroup)
{
	// Handles when no words were entered.
	if (wordGroup.Count == 0) return;

	// This makes sure that the blank line at the end of the group handling is written out only
	// if a group of words has been handled. (Ensures no print out of duplicate spaces).  
	if (firstGroupPrinted)
		Console.WriteLine();

	firstGroupPrinted = true;

	List<char[]> sortedWords = new List<char[]>();

	int longestWordLength = wordGroup.Max(x => x.Length);

	// Sorts the reversed words in the list with the first letter (last letter in non-reversed order),
	// Then by the second letter, and so on.. 
	sortedWords = wordGroup.OrderBy(x => new string(x)).ToList<char[]>();
		
	// Rebuilds the words into strings and right-justifies them
	foreach (char[] word in sortedWords)
	{
		// Reverts the order to the original word
		Array.Reverse(word);

		string wordString = string.Empty;

		// Rebuild the word 
		foreach (char letter in word)
			wordString = string.Concat(wordString, letter.ToString());

		if (word.Length == 0)
			continue;

		string rightJustifyWord = wordString.PadLeft(longestWordLength);

		Console.WriteLine($"{rightJustifyWord}");
	}

	// Prepares the list for a new group.
	words.Clear();
}


