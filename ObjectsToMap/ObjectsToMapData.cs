public static partial class ObjectsToMapData
{
    public static List<ObjectToMap> GetObjectsToMap(string connectionString)
    {
        var objectsToMap = new List<ObjectToMap>();

       
        objectsToMap.Add(Applications(connectionString));
        objectsToMap.Add(Languages(connectionString));
        objectsToMap.Add(TranslationTypes(connectionString));

        objectsToMap.Add(DefectQualityLevels(connectionString));
        objectsToMap.Add(QualityCategories(connectionString));
        objectsToMap.Add(DefectPieceRefs(connectionString));
        objectsToMap.Add(LogoutIdleTimer(connectionString));
        objectsToMap.Add(ManufacturingType(connectionString));
        
       

       

        foreach (var objectToMap in objectsToMap)
        {
            objectToMap.RecalculateColumnWidths();
        }

        return objectsToMap;
    }

}
