# Anagram
Simple Anagram Solver

Nothing at all fancy here. The aim is just to have something quite simple to work on for a bit, but also I'm challenging myself to not google or search stack overflow for potential sollutions. I have a very basic idea down for an anagram solver and I'm keen to see if it will work.

This all originally stemmed from a job interview I did many years ago that essentially involved working out the psuedo-code for an anagram solver and ever since I've had an itch wanting to "test" stuff out.

So my general plan is as follows...

* Simple console application
* Will use google for one thing...find a decent English dictionary!
* Will parse input and simply output anagrams of the word typed in.
	* First step being whole anagrams
	* Second step looking for all sub-words inside the inputted word

I plan to achieve this by aiming to give all "anagrams" the same hash key within a dictionary. So Dog and God would have the same hash key so looking up "Dog" in the dictionary would return a list of ["Dog", "God"] and likewise looking up "God" would return the same list.

So at its core, I will parse the English strings into a dictionary where Keys are hash values and Values are lists of words with that hash.


# Hashing and Single Word Anagram Lookup

For my first step, I'm simply going to take the word and arrange all the letter alphabetically, then take the hash value of that string.

So, "God" and "Dog" both arranged alphabetically....are "dgo" (I'll lower case everything first). So thats the key.

Any combination of these three letters typed into by the user will always resolve to the same key as arranged alphabetically they will always be "dgo".

Thats the plan anyway.


# Multiword Anagram Hunting

The above hashing worked great for just doing simple, whole word anagrams. But ideally it would be great to find all words inside a given input. So typing "goddog" would find "GOD DOG" as a possible sollution.

The rules being, ALL letters must be used in the sequence of words found, obviously with no letters being used more than once.

I do have a rudimentary version working using recursion, however with 10 letters or more it gets far too slow to be useable.

So how does it work?

For now, its inefficient. First it takes the input word and generates EVERY permutation of that string. (Yes. Its inefficient!)

Then for each permutation, it starts anagram hunting!

* It recursively runs on a string splitting it into two, Prefix and Suffix.
	* To start, Prefix is EMPTY and Suffix is the whole word.

The Recursive function works as so...
* It looks for all WHOLE anagrams of Prefix (one word anagrams)
	* Then it calls itself recursively using an Empty string Prefix and the whole Suffix
	* Each result returned by this is combined with each anagram found in Prefix
* Then it moves 1 char from the front of Suffix to the end of Prefix
* Then it re-runs recursively on the new longer Prefix and shorter Suffix.


This is all maybe over thought...so in a future date I might come back to this to rethink my approach and hopefully speed this up considerably.

I've done minor optimisations to cache lookups so that I never "de-anagram" the same string twice.

# Dictionary

This is the only thing I've allowed myself to google. For simplest start I thought a Scrabble dictionary might be the best place. So I've grabbed this one from jonbcard's github project...

https://raw.githubusercontent.com/jonbcard/scrabble-bot/master/src/dictionary.txt

# Demo

Here's just a brief demo of the anagram solver running...

![Testing Anagrams](https://github.com/IainS1986/Anagram/blob/master/GIFs/Anagram.gif)
