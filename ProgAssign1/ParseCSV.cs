using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ProgAssign1
{
    class ParseCSV
    {
        public static void readCombineCSV(string inputCsvFile)
        {
            int flag = 0;
            using (var reader = new StreamReader(inputCsvFile))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            }))
            using (var writer = new StreamWriter(DirectoryHandler.outputFilePath, true))
            using (var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            }))
            {
                List<Attributes> records = new List<Attributes>();

                try
                {
                    flag = 0;
                    records = csv.GetRecords<Attributes>().ToList();
                }
                catch (CsvHelper.HeaderValidationException ex)
                {
                    flag = 1;
                    Console.WriteLine("ERROR:    Header Validation Exception: " + ex.Message);
                }
                catch (CsvHelper.MissingFieldException ex)
                {
                    flag = 1;
                    Console.WriteLine("ERROR:    Missing Field Exception: " + ex.Message);
                }

                if (flag == 0)
                {
                    var filteredRecords = records.Where(record => !IsNotValid(record)).ToList();

                    foreach (var filterrecord in filteredRecords)
                    {
                        csvWriter.WriteRecord(filterrecord);
                        csvWriter.WriteField(extractDates(inputCsvFile));
                        csvWriter.NextRecord();
                        CountManager.IncrementProcessCount();
                        // Console.WriteLine($"INFO:   Processed Record - First Name: {filterrecord.FirstName}, Last Name: {filterrecord.LastName}, Street Number: {filterrecord.StreetNumber}, Street: {filterrecord.Street}, City: {filterrecord.City}, Province: {filterrecord.Province}, Postal Code: {filterrecord.PostalCode}, Country: {filterrecord.Country}, Phone Number: {filterrecord.PhoneNumber}, Email Address: {filterrecord.EmailAddress}");
                    }
                }
            }
        }

        public static void generateHeaders()
        {
            using (var writer = new StreamWriter(DirectoryHandler.outputFilePath, true))
            using (var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csvWriter.WriteField("First Name");
                csvWriter.WriteField("Last Name");
                csvWriter.WriteField("Street Number");
                csvWriter.WriteField("Street");
                csvWriter.WriteField("City");
                csvWriter.WriteField("Province");
                csvWriter.WriteField("Postal Code");
                csvWriter.WriteField("Country");
                csvWriter.WriteField("Phone Number");
                csvWriter.WriteField("Email Address");
                csvWriter.WriteField("Date");
                csvWriter.NextRecord();
            }
        }

        private static bool IsNotValid(Attributes record)
        {
            bool isNotValid = record.GetType().GetProperties().Any(property =>
            {
                var value = property.GetValue(record);
                if (value == null || (value is string str && string.IsNullOrEmpty(str)))
                {
                    return true;
                }

                if (property.Name == "FirstName" && value is string firstName)
                {
                    if (!Regex.IsMatch(firstName, "^[A-Za-z'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "LastName" && value is string lastName)
                {
                    if (!Regex.IsMatch(lastName, "^[A-Za-z\\s'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "Street" && value is string Street)
                {
                    if (!Regex.IsMatch(Street, "^[A-Za-z0-9\\s.'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "City" && value is string City)
                {
                    if (!Regex.IsMatch(City, "^[A-Za-z\\s.'-]+$|Saint-JÃ©rÃ´me|Trois-RiviÃ¨res|LÃ©vis"))
                    {
                        return true;
                    }
                }

                if (property.Name == "Province" && value is string Province)
                {
                    if (!Regex.IsMatch(Province, "^[A-Za-z\\s]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "Country" && value is string Country)

                {
                    if (!Regex.IsMatch(Country, "^[A-Za-z\\s'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "StreetNumber" && value is string StreetNumber)
                {
                    if (!Regex.IsMatch(StreetNumber, "^[0-9A-Za-z ]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "PostalCode" && value is string PostalCode)
                {
                    if (!Regex.IsMatch(PostalCode, @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d *$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "PhoneNumber" && value is string PhoneNumber)
                {
                    if (!Regex.IsMatch(PhoneNumber, "^[0-9]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "EmailAddress" && value is string EmailAddress)
                {
                    if (!Regex.IsMatch(EmailAddress, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                    {
                        return true;
                    }
                }

                return false;
            });

            if (isNotValid)
            {
                CountManager.IncrementSkipCount();
                Console.WriteLine($"INFO:   Skipped Record - First Name: {record.FirstName}, Last Name: {record.LastName}, Street Number: {record.StreetNumber}, Street: {record.Street}, City: {record.City}, Province: {record.Province}, Postal Code: {record.PostalCode}, Country: {record.Country}, Phone Number: {record.PhoneNumber}, Email Address: {record.EmailAddress}");
            }

            return isNotValid;
        }
        public static string extractDates(string path)
        {
            string regexPattern = @"\b\d{4}\\\d{1,2}\\\d{1,2}\b";
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(path);

            string capturedDate = match.Value.Replace("\\", "/");
            return capturedDate;
        }
    }
}
