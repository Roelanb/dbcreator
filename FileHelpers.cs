public static class FileHelpers
{
    public static FolderStructure CreateFolders(string generatedFolder, ObjectToMap objectToMap)
    {


        // add a folder called generated to the root of the project
        Directory.CreateDirectory(generatedFolder);
        // ui code generation


        Directory.CreateDirectory($@"{generatedFolder}/Features");
        Directory.CreateDirectory($@"{generatedFolder}/Features/" + objectToMap.UiFolderLevel1);

        var modelsFolder = "generated/Features/" + objectToMap.UiFolderLevel1 + "/Models";

        Directory.CreateDirectory(modelsFolder);
        Directory.CreateDirectory($@"{generatedFolder}/Features/" + objectToMap.UiFolderLevel1 + "/Pages");

        var editorsFolder = $@"{generatedFolder}/Features/" + objectToMap.UiFolderLevel1 + "/Pages/Editors";
        Directory.CreateDirectory(editorsFolder);


        var servicesFolder = $@"{generatedFolder}/Features/" + objectToMap.UiFolderLevel1 + "/Services";

        Directory.CreateDirectory(servicesFolder);

        Directory.CreateDirectory($@"{generatedFolder}/" + objectToMap.FolderLevel1);

        var controllersFolder = $@"{generatedFolder}/" + objectToMap.FolderLevel1 + "/Controllers";
        Directory.CreateDirectory(controllersFolder);
        Directory.CreateDirectory(controllersFolder + "/V1");

        var entitiesFolder = $@"{generatedFolder}/" + objectToMap.FolderLevel1 + "/Entities";
        Directory.CreateDirectory(entitiesFolder);

        var queriesFolder = $@"{generatedFolder}/" + objectToMap.FolderLevel1 + "/Queries";
        Directory.CreateDirectory(queriesFolder);

        var sharedFolder = $@"{generatedFolder}/" + objectToMap.FolderLevel1 + "/Shared";
        Directory.CreateDirectory(sharedFolder);

        var testsFolder = "tests/" + objectToMap.FolderLevel1;
        Directory.CreateDirectory(testsFolder);

        var databaseFolder = $@"{generatedFolder}/" + objectToMap.FolderLevel1 + "/Database";
        Directory.CreateDirectory(databaseFolder);




        // var navFolder = "generated/Features/Config/Pages/Navigation";
        // Directory.CreateDirectory(navFolder);

        
        var fs = new FolderStructure(controllersFolder, entitiesFolder, queriesFolder, sharedFolder, testsFolder, databaseFolder, modelsFolder, editorsFolder, servicesFolder);

        return fs;
    }
}

public record FolderStructure(string controllersFolder, string entitiesFolder, string queriesFolder, string sharedFolder, string testsFolder, 
            string databaseFolder,
            string modelsFolder, string editorsFolder, string servicesFolder);