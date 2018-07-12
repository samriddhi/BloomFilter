# BloomFilter
A bloom filter console application in C#

Visual Studio 2017.

This is a console application in C# to run a Bloom filter on a given set of numbers.
It tekes in 2 values:
1. The size of the bitmap (m). The default value is set at 10,000 if a non integer value is set.
2. The number of hashes (k). The default value is set at 1 if a non integer value is set.

Then reads a words.txt and hashing all the words in the file. The Hash function returns an array of k integers which then alter the bitmap accordingly. 
The words.txt file is included, it can be put anywhere in the system but the path will need to be updated in Program.cs file line 46.
As of right now the path is set to C:\\words.txt

Runing through many tests there are some observations:
1. If m is too little then there are alot more false positives as more values in the bitmap at 1.
2. Since an MD5 hash is used if the value of k is too high it takes some time to hash and add all the values.
3. Too large of a k value also returns back more values that will be set to 1 in the bitmap hence more false positives are present.



The theory of Bloom Filter is that a given word or term might exist if true. However if term is not found then 100% it doesnt exist, which was proven since certain words that do not exist in the list were still found.
Example: qwertyqwertyqwertysami did not exist in the array and when looking through the list it wasnt present.
 
