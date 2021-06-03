public class ConfigModel: Model
{
    private ConfigData data;
    public ConfigModel(): base("Config")
    {

    }

    public ConfigData Data { get => data; set => data = value; }
}
