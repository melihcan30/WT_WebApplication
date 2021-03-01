using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WTProject.model
{
    public class Team
    {
        private readonly string filePath;

        [JsonPropertyName("Students")]
        public List<Student> StudentInfos { get; set; }

        [JsonPropertyName("Extra")]
        public string Extra { get; set; }

        public class Student
        {
            [JsonPropertyName("Img")]
            public string ImgURL { get; set; }

            [JsonPropertyName("No")]
            public string No { get; set; }

            [JsonPropertyName("Name")]
            public string Name { get; set; }

            [JsonPropertyName("Surname")]
            public string Surname { get; set; }

            [JsonPropertyName("Age")]
            public int Age { get; set; }

            [JsonPropertyName("Course")]
            public string[] Course { get; set; }
        }

        public Team()
        {
            StudentInfos = new List<Student>();
            filePath = Path.Combine("wwwroot", "data", "student.json");
        }

        public Team(string team)
        {
            StudentInfos = new List<Student>();
            filePath = Path.Combine("wwwroot", "data", team + ".json");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Serialize()
        {
            JsonWriterOptions jsonWriterOptions = new JsonWriterOptions
            {
                Indented = true
            };

            using FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            using BufferedStream bufferedStream = new BufferedStream(fileStream);
            using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(bufferedStream, jsonWriterOptions);
            byte[] jsonByte = JsonSerializer.SerializeToUtf8Bytes<Team>(this);

            using JsonDocument jsonDocument = JsonDocument.Parse(jsonByte);
            JsonElement jsonElement = jsonDocument.RootElement;

            utf8JsonWriter.WriteStartObject();
            foreach (var item in jsonElement.EnumerateObject())
            {
                item.WriteTo(utf8JsonWriter);
            }
            utf8JsonWriter.WriteEndObject();
            utf8JsonWriter.Flush();
        }

        public Team Deserialize()
        {
            int numberOfBytes;
            byte[] buffer = new byte[4096];
            using FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            using BufferedStream bufferedStream = new BufferedStream(fileStream);
            Utf8JsonReader utf8JsonReader = new Utf8JsonReader(buffer, false, default);

            while ((numberOfBytes = bufferedStream.Read(buffer, 0, buffer.Length)) != 0)
                utf8JsonReader = new Utf8JsonReader(buffer, (numberOfBytes == 0), utf8JsonReader.CurrentState);

            return JsonSerializer.Deserialize<Team>(ref utf8JsonReader);
        }

        public override string ToString()
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            byte[] jsonByte = JsonSerializer.SerializeToUtf8Bytes<Team>(this, jsonSerializerOptions);
            return UTF8Encoding.UTF8.GetString(jsonByte);
        }
    }
 }
