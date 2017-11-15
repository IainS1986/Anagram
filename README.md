# Anagram
Simple Anagram Solver

Nothing at all fancy here. The aim is just to have something quite simple to work on for a bit, but also I'm challenging myself to not google or search stack overflow for potential sollutions. I have a very basic idea down for an anagram solver and I'm keen to see if it will work.

This all originally stemmed from a job interview I did many years ago that essentially involved working out the psuedo-code for an anagram solver and ever since I've had an itch wanting to "test" stuff out.

So my general plan is as follows...

* Simple console application
* Will use google for one thing...find a decent English dictionary!
* Will parse input and simply output anagrams of the word typed in.
** First step being whole anagrams
** Second step looking for all sub-words inside the inputted word

I plan to achieve this by aiming to give all "anagrams" the same hash key within a dictionary. So Dog and God would have the same hash key so looking up "Dog" in the dictionary would return a list of ["Dog", "God"] and likewise looking up "God" would return the same list.

So at its core, I will parse the English strings into a dictionary where Keys are hash values and Values are lists of words with that hash.


# Hashing

For my first step, I'm simply going to take the word and arrange all the letter alphabetically, then take the hash value of that string.

So, "God" and "Dog" both arranged alphabetically....are "dgo" (I'll lower case everything first). So thats the key.

Any combination of these three letters typed into by the user will always resolve to the same key as arranged alphabetically they will always be "dgo".


Thats the plan anyway.

# Recursion

My first aim is to just get simple whole word anagram solving. This I think should be somewhat trivial using the above approach to hashing.

The tricky part will come with looking for all subwords. So if the user typed in Good -> "Dog" and "God" would be returned (well, only if "o" was a word, which it wouldn't be).

So I could have 2 further features, one to return ALL words found inside the word (regardless of leftover unused words), and one to find lists of whole words that use ALL letters.

To do this, I'm envisaging having some sort of recursive function that takes a word, looks for anagrams of that word alone, then splits the word 1 character at a time and runs recursively on the two parts...

So, if the input was "Good" it would run,

"Good";
"G","ood";
"Go", "od";
"Goo", "d";
"G", "o", "od";
"G", "o", "o", "d";
"Go", "o", "d"

Although...typing that out I realise I've done that wrong, it would of course be in alphabetical order first!

"dgoo";
"d","goo";
"dg","oo";
"dgo";"o";
"d","g","oo";
"d","g","o","o";
"dg","o","o";

And of course, there are many duplicates in there so repeated solves can be cached and returned much faster.

