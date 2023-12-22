using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Error. Two arguments are needed.");
            return;
        }

        string testsJsonPath = args[0];
        string valuesJsonPath = args[1];

        try
        {
            string testsJson = File.ReadAllText(testsJsonPath);
            string valuesJson = File.ReadAllText(valuesJsonPath);

            JObject testsObject = JsonConvert.DeserializeObject<JObject>(testsJson);
            JObject valuesObject = JsonConvert.DeserializeObject<JObject>(valuesJson);

            ProcessTests(testsObject, valuesObject);

            string reportJson = JsonConvert.SerializeObject(testsObject, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented);
            File.WriteAllText("report.json", reportJson);

            Console.WriteLine("Report generated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void ProcessTests(JObject testsObject, JObject valuesObject)
    {
        JToken testsToken = testsObject["tests"];
        if (testsToken != null && testsToken.Type == JTokenType.Array)
        {
            foreach (JObject test in testsToken.Children<JObject>())
            {
                UpdateTestValues(test, valuesObject);
            }
        }
    }

    static void UpdateTestValues(JObject test, JObject valuesObject)
    {
        JToken testIdToken = test["id"];
        if (testIdToken != null && testIdToken.Type == JTokenType.Integer)
        {
            int testId = testIdToken.Value<int>();

            JToken valueToken = valuesObject["values"]
                .FirstOrDefault(v => v["id"].Value<int>() == testId);

            if (valueToken != null)
            {
                test["value"] = valueToken["value"].Value<string>();
            }

            JToken nestedTestsToken = test["values"];
            if (nestedTestsToken != null && nestedTestsToken.Type == JTokenType.Array)
            {
                foreach (JObject nestedTest in nestedTestsToken.Children<JObject>())
                {
                    UpdateTestValues(nestedTest, valuesObject);
                }
            }
        }
    }
}
