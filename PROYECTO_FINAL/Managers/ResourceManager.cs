using SFML.Graphics;

static class ResourceManager
{
    static private Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

    static public Texture CreateTexture(string filePath)
    {
        if (!textures.ContainsKey(filePath))
        {
            textures.Add(filePath, new Texture(filePath));
        }
        return textures[filePath];
    }
}