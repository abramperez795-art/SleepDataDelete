
// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data file.");
Console.WriteLine("Enter anything else to quit");
//input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // TODO: create data file
    // ask question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks Sunday - Saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    //subtract # weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    //Console.WriteLine(dataDate);
    // random number generator
    Random rnd = new();
    // Create file
   using StreamWriter sw = new("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week 
        int[] hours = new int[7];
        for (int i = 0; i < 7; i++)
        {
            //generate random number between 4-12 inclusive
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy, #|#|#|#|#|#|#
        //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
   // sw.Close();
}
else if (resp == "2")
{
    // TODO: Parse data file
    var lines = File.ReadAllLines("data.txt");
    foreach (var line in lines)
    {
        
  if (string.IsNullOrWhiteSpace(line))
            continue;

        // Split date and data
        var parts = line.Split(',');
        var date = DateTime.Parse(parts[0]);
        var sleepHours = parts[1].Split('|').Select(int.Parse).ToArray();
        int total = sleepHours.Sum();
        double avg = Math.Round((double)total / sleepHours.Length, 1);

         // Output
        Console.WriteLine($"Week of {date:MMM, dd, yyyy}");
        Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
        Console.WriteLine(" -- -- -- -- -- -- -- --- ---");
        Console.WriteLine(" " +
            string.Join(" ", sleepHours.Select(h => h.ToString().PadLeft(2))) +
            $" {total.ToString().PadLeft(3)} {avg.ToString("0.0").PadLeft(3)}");
        Console.WriteLine();
    }
}