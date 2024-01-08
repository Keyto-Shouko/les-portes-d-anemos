using System;

[Serializable]
public class SerializableTeleporter
{
    public string name;
    public string description;
    public float positionX;
    public float positionY;
    // Add any other fields you want to save

    // Constructor to initialize fields
    public SerializableTeleporter(string name, string description, float positionX, float positionY)
    {
        this.name = name;
        this.description = description;
        this.positionX = positionX;
        this.positionY = positionY;
        // Initialize any other fields
    }
}
