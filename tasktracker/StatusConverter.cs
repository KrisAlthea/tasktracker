using System;

namespace tasktracker;

using System.Text.Json;
using System.Text.Json.Serialization;

public class StatusConverter : JsonConverter<Status>
{
    public override Status Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? enumString = reader.GetString();

        return enumString switch
        {
            "todo" => Status.todo,
            "in-progress" => Status.in_progress,
            "done" => Status.done,
            _ => throw new JsonException($"Invalid status value: {enumString}")
        };
    }

    public override void Write(Utf8JsonWriter writer, Status value, JsonSerializerOptions options)
    {
        string stringValue = value switch
        {
            Status.todo => "todo",
            Status.in_progress => "in-progress",
            Status.done => "done",
            _ => throw new JsonException($"Invalid status value: {value}")
        };
        writer.WriteStringValue(stringValue);
    }
}