using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEAParser
{
    /*
    ************************ NOTE! ************************
    This class was built only for test purposes and parsing only time, latitude and longitude values.
    Validation should work fine though.
    */
    public class GPGGAMessage
    {
        public TimeSpan Time { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static GPGGAMessage Parse(string sentence, bool validateCheckSum = true)
        {
            var fields = ValidateSentence(sentence, validateCheckSum);

            TimeSpan sentenceTime = GetTime(fields[1]);
            double latitude = GetLatitude(fields[2], fields[3]);
            double longitude = GetLongtitude(fields[4], fields[5]);

            return new GPGGAMessage() { Time = sentenceTime, Latitude = latitude, Longitude = longitude };
        }

        private static double GetLatitude(string latitudeField, string indicatorField)
        {
            latitudeField = latitudeField.Remove(4, 1).Insert(2, ".");
            double latitude = Convert.ToDouble(latitudeField);
            if (indicatorField == "S") latitude *= -1;
            return latitude;
        }

        private static double GetLongtitude(string longitudeField, string indicatorField)
        {
            longitudeField = longitudeField.Remove(5, 1).Insert(3, ".");
            double longitude = Convert.ToDouble(longitudeField);
            if (indicatorField == "W") longitude *= -1;
            return longitude;
        }

        private static TimeSpan GetTime(string timeField)
        {
            int hours = int.Parse(timeField.Substring(0,2));
            int minutes = int.Parse(timeField.Substring(2,2));
            int seconds = int.Parse(timeField.Substring(4,2));

            return new TimeSpan(0, hours, minutes, seconds);
        }

        private static string[] ValidateSentence(string sentence, bool validateCheckSum)
        {
            if(string.IsNullOrWhiteSpace(sentence)) throw new Exception("Sentence is empty.");
            if (sentence.Length > 82) throw new Exception("Format violation: sentence is more than 82 characters long.");

            sentence = sentence.TrimEnd(); //if there is a carriage return/line feed at the end
            if (sentence.StartsWith("$") == false) throw new Exception("Format violation: sentence is not starting with delimeter '$'.");
            int checkSumDelimeterIndex = sentence.IndexOf('*');
            if (checkSumDelimeterIndex == -1) throw new Exception("Format violation: sentence does not have checksum delimeter '*'.");

            if (validateCheckSum) ValidateCheckSum(sentence, checkSumDelimeterIndex);

            string[] fields = sentence.Remove(checkSumDelimeterIndex).Split(',');
            if (fields.Length != 15) throw new Exception("Format violation: sentence must be 15 fields long before checksum delimeter '*' and separated by a commas.");

            if (fields[0] != "$GPGGA") throw new Exception($"It is not a GPGGA message but {fields[0]}. Use an appropriate class type for parsing.");

            return fields;
        }

        private static void ValidateCheckSum(string sentence, int checkSumDelimeterIndex)
        {
            string t = sentence.Substring(sentence.Length - 2);
            int sentenceChecksum = Convert.ToInt32(t, 16);
            string mainPart = sentence.Substring(1).Remove(checkSumDelimeterIndex - 1);
            int calculatedChecksum = CalculateChecksum(mainPart);
            if (calculatedChecksum != sentenceChecksum) throw new Exception("Calculated checksum and checksum provided in sentence are not matching.");
        }

        private static int CalculateChecksum(string mainPart)
        {
            byte checkSum = 0x00;
            byte[] content = Encoding.UTF8.GetBytes(mainPart);
            for(int i =  0; i < content.Length; i++)
            {
                checkSum ^= content[i];
            }

            return checkSum;
        }
    }


}
