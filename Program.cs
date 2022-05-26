// Virginia Lu
// C# Assignment 1

using System.Text;

// UNDERSTANDING DATA TYPES

// Test your Knowledge

// 1. What type would you choose for:
//      - A person's telephone number: string (if like +1(###)###-####), int (if like ##########)
//      - A person's height: float
//      - A person's age: int
//      - A person's gender: string
//      - A person's salary: decimal
//      - A book's ISBN: string
//      - A book's price: decimal
//      - A book's shipping weight: float
//      - A country's population: int
//      - The number of stars in the universe: long
//      - The number of employees in each of the small or medium businesses in the UK: int
// 2. What are the differences between value type and reference type variables? What is boxing and unboxing?
//      - Value type varaibles directly contain their data while reference type variables store references to their data/objects. Each value type variable has its own copy of data, and more than one reference type variable can reference the same object. Also, operation on one value type variable cannot affect another, while operation on one reference type variable can.
//      - Boxing is the process in which a value type is converted into a reference type. Unboxing is the reverse process in which a reference type is converted into a value type.
// 3. What is meant by the terms managed resource and unmanaged resource in .NET?
//      - Managed resources are managed by Garbarge Collector. Unmanaged resources are files, streams, database connections, etc. not managed by Garbage Collector. You have to call Dispose() from the IDisposable interface to release them.
// 4. What's the purpose of Garbage Collector in .NET?
//      - The garbage collector manages the allocation and release of memory for an application. For developers working with managed code, this means that you don't have to write code to perform memory management tasks. Automatic memory management can eliminate common problems, such as forgetting to free an object and causing a memory leak or attempting to access memory for an object that's already been freed.

// ----------------------------------------------------------------------------

// Playing with Console App

Console.WriteLine("Favorite color: ");
string color = Console.ReadLine();
Console.WriteLine("Astrology sign: ");
string sign = Console.ReadLine();
Console.WriteLine("Street address number: ");
string number = Console.ReadLine();
Console.WriteLine($"Your hacker name is {color}{sign}{number}");

// ----------------------------------------------------------------------------

// Practice number sizes and ranges

// 1.
Console.WriteLine($"Type: sbyte, Number of bytes: {sizeof(sbyte)}, Min value: {sbyte.MinValue}, Max value: {sbyte.MaxValue}");
Console.WriteLine($"Type: byte, Number of bytes: {sizeof(byte)}, Min value: {byte.MinValue}, Max value: {byte.MaxValue}");
Console.WriteLine($"Type: short, Number of bytes: {sizeof(short)}, Min value: {short.MinValue}, Max value: {short.MaxValue}");
Console.WriteLine($"Type: ushort, Number of bytes: {sizeof(ushort)}, Min value: {ushort.MinValue}, Max value: {ushort.MaxValue}");
Console.WriteLine($"Type: int, Number of bytes: {sizeof(int)}, Min value: {int.MinValue}, Max value: {int.MaxValue}");
Console.WriteLine($"Type: uint, Number of bytes: {sizeof(uint)}, Min value: {uint.MinValue}, Max value: {uint.MaxValue}");
Console.WriteLine($"Type: long, Number of bytes: {sizeof(long)}, Min value: {long.MinValue}, Max value: {long.MaxValue}");
Console.WriteLine($"Type: ulong, Number of bytes: {sizeof(ulong)}, Min value: {ulong.MinValue}, Max value: {ulong.MaxValue}");
Console.WriteLine($"Type: float, Number of bytes: {sizeof(float)}, Min value: {float.MinValue}, Max value: {float.MaxValue}");
Console.WriteLine($"Type: double, Number of bytes: {sizeof(double)}, Min value: {double.MinValue}, Max value: {double.MaxValue}");
Console.WriteLine($"Type: decimal, Number of bytes: {sizeof(decimal)}, Min value: {decimal.MinValue}, Max value: {decimal.MaxValue}");

// ----------------------------------------------------------------------------

// 2.
// centuries, years, days, hours, minutes --> int
// seconds, milliseconds, microseconds --> long
// nanoseconds --> ulong
Console.WriteLine("Input: ");
string userInput = Console.ReadLine();
int centuries = Convert.ToInt32(userInput);
int years = centuries * 100;
int days = (int)(years * 365.2425);
int hours = days * 24;
int minutes = hours * 60;
long seconds = (long)(minutes) * 60;
long milliseconds = seconds * 1000;
long microseconds = milliseconds * 1000;
ulong nanoseconds = (ulong)(microseconds) * 1000;
Console.WriteLine($"Output: {centuries} centuries = {years} years = {days} days = {hours} hours = {minutes} minutes = {seconds} seconds = {milliseconds} milliseconds = {microseconds} microseconds = {nanoseconds} nanoseconds");


// ----------------------------------------------------------------------------


//CONTROLLING FLOW AND CONVERTING TYPES

//Test your Knowledge

// 1. What happens when you divide an int variable by 0?
//      - An exception is thrown: System.DivideByZeroException: Attempted to divide by zero
// 2. What happens when you divide a double variable by 0?
//      - Output is infinity
// 3. What happens when you overflow an int variable?
//      - In a checked context, an OverflowException is thrown. In an unchecked context, the most significant bits of the result are discarded and execution continues.
// 4. What is the difference between x = y++; and x = ++y;?
//      -Both ways increments y by 1. The difference is that y++ is used as a post-increment operator meaning y is used before incrementing and ++y is used as a pre-increment operator meaning y is incremented then used.
// 5.  What is the difference between break, continue, and return when used inside a loop statement?
//      - Break is used to exit from the current loop. Continue is used to move the control to the next iteration of the loop. Return is used to return a function value or to terminate the execution of a function.
// 6. What are the three parts of a for statement and which of them are required?
//      - The initialization, condition, and iterator are the three parts of a for statement, for example: for (int i = 0; i < 100; i++).All three parts are optional, as you can initialize a variable before the loop and both the condition and iterator can be defined inside the code block.
// 7. What is the difference between the = and == operators?
//      - The = operator assigns values to a variable, while == is the equality operator that returns a boolean value (true if its operands are equal, false otherwise).
// 8.Does the following statement compile? for (; true;);
//-Yes, but it executes an infinite loop.
// 9. What does the underscore _ represent in a switch expression?
//      - The underscore character replaces the default keyword to signify that it should match anything if reached.
// 10. What interface must an object implement to be enumerated over by using the foreach statement ?
//      -The IEnumerable interface.

// ----------------------------------------------------------------------------

// Practice loops and operators

// 1. FizzBuzz
for (int i = 1; i <= 100; i++)
{
    string ans = "";
    if (i % 3 == 0 && i % 5 == 0)
    {
        ans = "FizzBuzz";
    }
    else if (i % 3 == 0)
    {
        ans = "Fizz";
    }
    else if (i % 5 == 0)
    {
        ans = "Buzz";
    }
    else
    {
        ans = $"{i}";
    }
    Console.WriteLine($"{ans}");
}

// ----------------------------------------------------------------------------

// Exercise
// Output never stops, because byte i always < 500.
int max = 500;
if (max <= byte.MaxValue)
{
    for (byte i = 0; i < max; i++)
    {
        Console.WriteLine(i);
    }
}
else
{
    Console.WriteLine("Warning: Infinte loop. Max value exceeds byte type max.");
}

// ----------------------------------------------------------------------------

// 2. Print-a-Pyramid
for (int i = 1; i < 6; i++)
{
    int numSpaces = 5 - i;
    string spaces = "";
    for (int j = 1; j <= numSpaces; j++)
    {
        spaces += " ";
    }

    int numStars = 2 * i - 1;
    string stars = "";
    for (int k = 1; k <= numStars; k++)
    {
        stars += "*";
    }
    Console.WriteLine(spaces + stars + spaces);
}

// ----------------------------------------------------------------------------

// 3.
int correctNumber = new Random().Next(3) + 1;
Console.WriteLine("Guess a number between 1 and 3.");
int guess = Convert.ToInt32(Console.ReadLine());
if (guess < correctNumber)
{
    Console.WriteLine("You guessed low.");
    if (guess < 1)
    {
        Console.WriteLine("And your answer is outside the range of valid guesses.");
    }
}
else if (guess > correctNumber)
{
    Console.WriteLine("You guessed high.");
    if (guess > 3)
    {
        Console.WriteLine("And your answer is outside the range of valid guesses.");
    }
}
else
{
    Console.WriteLine("You guessed the correct number!");
}

// ----------------------------------------------------------------------------

// 4.
DateTime bday = new DateTime(1997, 7, 31);
int ageInDays = (int)((DateTime.Now - bday).TotalDays);
Console.WriteLine($"{ageInDays} days old");

int daysToNextAnniversary = 10000 - (ageInDays % 10000);
Console.WriteLine($"{daysToNextAnniversary} days until next 10,000 day anniversary");

DateTime nextAnniversary = DateTime.Now.AddDays(daysToNextAnniversary);
Console.WriteLine($"Date of next 10,000 day anniversary: {nextAnniversary}");

// ----------------------------------------------------------------------------

// 5.
DateTime currentTime = DateTime.Now;
int hour = currentTime.Hour;
if (hour >= 4 && hour < 12)
{
    Console.WriteLine("Good Morning");
}
else if (hour >= 12 && hour < 17)
{
    Console.WriteLine("Good Afternoon");
}
else if (hour >= 17 && hour < 22)
{
    Console.WriteLine("Good Evening");
}
else
{
    Console.WriteLine("Good Night");
}

// ----------------------------------------------------------------------------

// 6.
for (int i = 1; i < 5; i++)
{
    string toPrint = "";
    for (int j = 0; j < 25; j += i)
    {
        toPrint += $"{j} ";
    }
    Console.WriteLine(toPrint);
}