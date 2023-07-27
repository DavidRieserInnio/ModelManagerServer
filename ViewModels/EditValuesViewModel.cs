namespace ModelManagerServer.ViewModels
{
    public record EditValuesViewModel(
        Guid ModelId, 
        int ModelVersion, 
        List<(string, string)> Values
    )
    {

    }
}
