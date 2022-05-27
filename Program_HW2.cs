// Virginia Lu, C# Assignment 2

using System;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

// ARRAY AND STRINGS

// Test your Knowledge
// 1. When to use String vs. StringBuilder in C#?
//      - If a string is going to remain constant throughout the program, then use String class object because a String object is immutable. If a string can change (example: lots of logic and operations in the construction of the string) then using a StringBuilder is the best option.
// 2. What is the base class for all arrays in C#?
//      - The Array class
// 3. How do you sort an array in C#?
//      - Use method Array.Sort()
// 4. What property of an array object can be used to get the total number of elements in an array?
//      - Array.Length
// 5. Can you store multiple data types in System.Array?
//      - No
// 6. What's the difference between System.Array.CopyTo() and System.Array.Clone()?
//      - CopyTo(Array, Int32) copies all the elements of the current one-dimensional Array to the specified one-dimensional Array starting at the specified destination Array index. The index is specified as a 32-bit integer.
//      - Clone() creates a shallow copy of the Array.


// -----------------------------------------------------------------------------


// PRACTICE ARRAYS

// 1. Copying an Array
Array array1 = Array.CreateInstance(typeof(int), 10);
for (int i = array1.GetLowerBound(0); i <= array1.GetUpperBound(0); i++)
{
    array1.SetValue(i + 1, i);
}

Array array2 = Array.CreateInstance(typeof(int), array1.Length);
for (int i = array2.GetLowerBound(0); i <= array2.GetUpperBound(0); i++)
{
    array2.SetValue(array1.GetValue(i), i);
}

foreach (int i in array1)
{
    Console.Write("{0} ", i);
}
Console.WriteLine("");
foreach (int i in array2)
{
    Console.Write("{0} ", i);
}

// -----------------------------------------------------------------------------

// 2.
String[] myList = { };
for (int i = 0; i >= 0; i++)
{
    //string item = "";
    Console.WriteLine("Enter command (+ item, - item, or -- to clear):");
    string command = Console.ReadLine();
    Console.WriteLine("");
    string item = command.Substring(2);
    if (command[0] == '+')
    {
        // add item
        Array.Resize(ref myList, myList.Length + 1);
        myList[myList.Length - 1] = item;
    }
    else if (command[0] == '-')
    {
        if (command[1] == '-')
        {
            // clear list
            Array.Clear(myList);
            Array.Resize(ref myList, 0);
        }
        else
        {
            // remove item
            if (Array.Exists(myList, element => element == item))
            {
                int index = Array.IndexOf(myList, item);
                Array.Clear(myList, index, 1);
                for (i = index; i < myList.Length - 1; i++)
                {
                    myList.SetValue(myList[i + 1], i);
                }
                Array.Resize(ref myList, myList.Length - 1);
            }
            else
            {
                Console.WriteLine("Item does not currently exist in your list.");
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid command.");
    }
    Console.WriteLine("Your List:");
    for (i = 0; i < myList.Length; i++)
    {
        Console.WriteLine(myList[i]);
    }
    Console.WriteLine("");
}

// -----------------------------------------------------------------------------

// 3.
static int[] FindPrimesInRange(int startNum, int endNum)
{
    List<int> primeList = new List<int>();
    for (int i = startNum; i <= endNum; i++)
    {
        int counter = 0;
        for (int j = 2; j <= i / 2; j++)
        {
            if (i % j == 0)
            {
                counter++;
                break;
            }
        }
        if (counter == 0 && i != 1)
        {
            primeList.Add(i);
        }
    }
    int[] primeArray = primeList.ToArray();
    return primeArray;
}

//int[] primesBetween1and50 = FindPrimesInRange(1, 50);
//Array.ForEach(primesBetween1and50, Console.WriteLine);

// -----------------------------------------------------------------------------

// 4.
static int[] sumRotatedArrays(int[] inputArray, int k)
{
    int n = inputArray.Length;
    int[] outputArray = new int[n];
    for (int r = 1; r <= k; r++)
    {
        int[] rotatedArray = new int[n];
        for (int i = 0; i < n; i++)
        {
            int newIndex = (i + r) % n;
            rotatedArray.SetValue(inputArray[i], newIndex);
        }
        outputArray = outputArray.Zip(rotatedArray, (x, y) => x + y).ToArray();
    }
    return outputArray;

}

//int[] test = sumRotatedArrays(new int[] { 3, 2, 4, -1 }, 2);
//Array.ForEach(test, Console.WriteLine);

// -----------------------------------------------------------------------------

// 5.
static int[] longestSequence(int[] inputArray)
{
    int count = 1;
    int longestNum = inputArray[0];
    int longestCount = 1;
    for (int i = 0; i < inputArray.Length - 1; i++)
    {
        if (inputArray[i] != inputArray[i + 1])
        {
            count = 0;
        }
        count++;
        if (count > longestCount)
        {
            longestCount = count;
            longestNum = inputArray[i];
        }
    }
    int[] outputArray = new int[longestCount];
    Array.Fill(outputArray, longestNum);
    return outputArray;
}

//int[] test = longestSequence(new int[] { 0,1,1,5,2,2,6,3,3,3 });
//Array.ForEach(test, Console.WriteLine);

// -----------------------------------------------------------------------------

// 7.
static void MostCommon(int[] numbers)
{
    var max = (numbers.GroupBy(x => x)
        .Select(x => new { num = x, cnt = x.Count() })
        .OrderByDescending(g => g.cnt)
        .Select(g => g.num).First());

    Console.WriteLine("The leftmost most frequent number is {0} (occurs {1} times)",
        max.Key, max.Count());
}

//MostCommon(new int[] { 7, 7, 7, 0, 2, 2,2,0,10,10,10 });


// -----------------------------------------------------------------------------


// PRACTICE STRINGS

// 1.
static void reverseString1()
{
    Console.WriteLine("Input:");
    string forwardString = Console.ReadLine();
    char[] charArr = forwardString.ToCharArray();
    Array.Reverse(charArr);
    string reversedString = new string(charArr);
    Console.WriteLine(reversedString);
}

//reverseString1();

static void reverseString2()
{
    Console.WriteLine("Input:");
    string forwardString = Console.ReadLine();
    for (int i = forwardString.Length - 1; i >= 0; i--)
    {
        Console.Write(forwardString[i]);
    }
}

//reverseString2();

// -----------------------------------------------------------------------------

//// 2.
//static void reverseString(string str)
//{
//    char[] listOfSeperators = new char[] { ' ', '.', ',', ':', ';', '=', '(', ')', '&', '[', ']', '"', '\'', '/', '\\', '!', '?' };
//    //string reversedStr = String.Join(" ", str.Split(listOfSeperators).Reverse());
//    string[] strSplitBySpaces = str.Split(' ');
//    foreach (string s in strSplitBySpaces)
//    {
//        if (listOfSeperators.Any(s.Contains))
//        {
//            string word = s.Split(listOfSeperators)[0];
//            string[] seperators = s.Split(word);
//        }
//    }
//string reversedStr = String.Join(" ", str.Split(' ').Reverse());
//Console.WriteLine(reversedStr);
//string punctuation = String.Join(" ", str.Split(' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z')).ToString();
//Console.WriteLine(punctuation);
//}

//reverseString("C# is not C++, and PHP is not Delphi!");

//static void ReverseString(string s)
//{
//    var sb = new StringBuilder();

//    char[] listOfSeperators = new char[] { ' ', '.', ',', ':', ';', '=', '(', ')', '&', '[', ']', '"', '\'', '/', '\\', '!', '?' };

//    foreach (string s1 in s.Split(' ').Reverse())
//    {
//        //var rev = s1.ToList();
//        //char punct;
//        //if (char.IsPunctuation(punct = rev.First()))
//        if (listOfSeperators.Any(s1.Contains))
//        {
//            for 
//            rev.RemoveAt(0);
//            rev.Add(punct);
//        }
//        rev.Add(' ');
//        sb.Append(rev.ToArray());
//    }
//    //return sb.ToString();
//    Console.WriteLine(sb.ToString());
//}

//ReverseString("C# is not C++, and PHP is not Delphi!");

//string test = "!hello?";
//string[] array = test.Split("hello");
//Array.ForEach(array, Console.WriteLine);
//Console.WriteLine(array.Length);

//string s1 = "hello";
//char[] rev = s1.ToCharArray();
//Array.ForEach(rev, Console.WriteLine);
