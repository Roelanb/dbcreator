//     var connectionString = "server=.;database=MES;Persist Security Info=False;Integrated Security=True;TrustServerCertificate=True;";            

// var test = ObjectsToMapData.LogoutIdleTimer(connectionString);

// test.LoadColumnsFromSqlCreateCommand();

// return;

var generateBackend = true;
var generateFrontend = true;
var generateNavigation = true;

var cleanUpFiles = true;

var converter = new Converter();

var objectsToMap = ObjectsToMapData.GetObjectsToMap();

// add a folder called generated to the root of the project
Directory.CreateDirectory("generated");

var infrastructureApiFileText = converter.GenerateApiInfrastructureString(objectsToMap);

var infrastructureFolder = "generated/Infrastructure";
Directory.CreateDirectory(infrastructureFolder);
File.WriteAllText(infrastructureFolder + "/ServiceCollectionExtensions.cs", infrastructureApiFileText);

foreach (var objectToMap in objectsToMap)
{
    Directory.CreateDirectory("generated/" + objectToMap.FolderLevel1);

    var controllersFolder = "generated/" + objectToMap.FolderLevel1 + "/Controllers";
    Directory.CreateDirectory(controllersFolder);
    Directory.CreateDirectory(controllersFolder + "/V1");

    var entitiesFolder = "generated/" + objectToMap.FolderLevel1 + "/Entities";
    Directory.CreateDirectory(entitiesFolder);

    var queriesFolder = "generated/" + objectToMap.FolderLevel1 + "/Queries";
    Directory.CreateDirectory(queriesFolder);

    var sharedFolder = "generated/" + objectToMap.FolderLevel1 + "/Shared";
    Directory.CreateDirectory(sharedFolder);

    var testsFolder = "generated/" + objectToMap.FolderLevel1 + "/Tests";
    Directory.CreateDirectory(testsFolder);

    var databaseFolder = "generated/" + objectToMap.FolderLevel1 + "/Database";
    Directory.CreateDirectory(databaseFolder);

    if (generateBackend)
    {
        var entityFileText = converter.GenerateEntityCSharpClass(objectToMap);

        File.WriteAllText(entitiesFolder + "/" + objectToMap.EntityName + ".cs", entityFileText);

        var repoFileText = converter.GenerateRepositoryClassString(objectToMap);

        File.WriteAllText(sharedFolder + "/" + objectToMap.Name + "Repository.cs", repoFileText);

        var iRepoFileText = converter.GenerateIRepositoryClassString(objectToMap);

        File.WriteAllText(sharedFolder + "/I" + objectToMap.Name + "Repository.cs", iRepoFileText);

        var queryFileText = converter.GenerateQueryClassString(objectToMap);

        File.WriteAllText(queriesFolder + "/" + objectToMap.Name + "Query.cs", queryFileText);

        var controllerFileText = converter.GenerateControllerClassString(objectToMap);

        File.WriteAllText(controllersFolder + "/V1/" + objectToMap.Name + "Controller.cs", controllerFileText);

        var exampleRestCallsFileText = converter.GenerateExampleRestCallsString(objectToMap);

        File.WriteAllText(testsFolder + "/" + objectToMap.Name + "RestCalls.http", exampleRestCallsFileText);

        converter.GenerateDatabaseObjects(databaseFolder, objectToMap);


    }

    // ui code generation

    Directory.CreateDirectory("generated/Features");
    Directory.CreateDirectory("generated/Features/" + objectToMap.UiFolderLevel1);

    var modelsFolder = "generated/Features/" + objectToMap.UiFolderLevel1 + "/Models";

    Directory.CreateDirectory(modelsFolder);
    Directory.CreateDirectory("generated/Features/" + objectToMap.UiFolderLevel1 + "/Pages");

    var editorsFolder = "generated/Features/" + objectToMap.UiFolderLevel1 + "/Pages/Editors";
    Directory.CreateDirectory(editorsFolder);


    var servicesFolder = "generated/Features/" + objectToMap.UiFolderLevel1 + "/Services";

    Directory.CreateDirectory(servicesFolder);

    if (generateFrontend)
    {
        var modelFileText = converter.GenerateModelClassString(objectToMap);

        File.WriteAllText(modelsFolder + "/" + objectToMap.EntityName + ".cs", modelFileText);

        var iserviceFileText = converter.GenerateIServicesClassString(objectToMap);

        File.WriteAllText(servicesFolder + "/I" + objectToMap.Name + "Service.cs", iserviceFileText);

        var serviceFileText = converter.GenerateServicesClassString(objectToMap);

        File.WriteAllText(servicesFolder + "/" + objectToMap.Name + "Service.cs", serviceFileText);

        var editorFileText = converter.GenerateEditorRazorClassString(objectToMap);

        File.WriteAllText(editorsFolder + "/" + objectToMap.Name + "Editor.razor", editorFileText);

        var editorCodeFileText = converter.GenerateEditorClassString(objectToMap);

        File.WriteAllText(editorsFolder + "/" + objectToMap.Name + "Editor.razor.cs", editorCodeFileText);




    }

}

if (generateBackend)
{
    // copy all files from the generated folder to the project folder
    var source = @"D:\Projects\crud\dbcreator\generated";
    var destination = @"D:\Projects\crud\CrudApi\DataService.Config";

    foreach (var objectToMap in objectsToMap)
    {
        var sourceFolder = source + "\\" + objectToMap.FolderLevel1 + "\\Controllers\\V1";
        var destinationFolder = destination + "\\Features\\" + objectToMap.FolderLevel1 + "\\Controllers\\V1";

        // create the desitonation folder
        Directory.CreateDirectory(destinationFolder);

        // copy the file with *.cs extension from source to destination
        foreach (string dirPath in Directory.GetFiles(sourceFolder, "*.cs", SearchOption.TopDirectoryOnly))
        {
            Console.WriteLine(dirPath);
            Console.WriteLine(dirPath.Replace(sourceFolder, destinationFolder));

            File.Copy(dirPath, dirPath.Replace(sourceFolder, destinationFolder), true);
        }

    }

    foreach (var objectToMap in objectsToMap)
    {
        var sourceFolder = source + "\\" + objectToMap.FolderLevel1 + "\\Entities";
        var destinationFolder = destination + "\\Features\\" + objectToMap.FolderLevel1 + "\\Entities";

        // create the desitonation folder
        Directory.CreateDirectory(destinationFolder);

        // copy the file with *.cs extension from source to destination
        foreach (string dirPath in Directory.GetFiles(sourceFolder, "*.cs", SearchOption.TopDirectoryOnly))
        {
            Console.WriteLine(dirPath);
            Console.WriteLine(dirPath.Replace(sourceFolder, destinationFolder));

            File.Copy(dirPath, dirPath.Replace(sourceFolder, destinationFolder), true);
        }
    }

    foreach (var objectToMap in objectsToMap)
    {
        var sourceFolder = source + "\\" + objectToMap.FolderLevel1 + "\\Queries";
        var destinationFolder = destination + "\\Features\\" + objectToMap.FolderLevel1 + "\\Queries";

        // create the desitonation folder
        Directory.CreateDirectory(destinationFolder);

        // copy the file with *.cs extension from source to destination
        foreach (string dirPath in Directory.GetFiles(sourceFolder, "*.cs", SearchOption.TopDirectoryOnly))
        {
            Console.WriteLine(dirPath);
            Console.WriteLine(dirPath.Replace(sourceFolder, destinationFolder));

            File.Copy(dirPath, dirPath.Replace(sourceFolder, destinationFolder), true);
        }
    }


    foreach (var objectToMap in objectsToMap)
    {
        var sourceFolder = source + "\\" + objectToMap.FolderLevel1 + "\\Shared";
        var destinationFolder = destination + "\\Features\\" + objectToMap.FolderLevel1 + "\\Shared";

        // create the desitonation folder
        Directory.CreateDirectory(destinationFolder);

        // copy the file with *.cs extension from source to destination
        foreach (string dirPath in Directory.GetFiles(sourceFolder, "*.cs", SearchOption.TopDirectoryOnly))
        {
            Console.WriteLine(dirPath);
            Console.WriteLine(dirPath.Replace(sourceFolder, destinationFolder));

            File.Copy(dirPath, dirPath.Replace(sourceFolder, destinationFolder), true);
        }
    }

    foreach (var objectToMap in objectsToMap)
    {
        var sourceFolder = source + "\\" + objectToMap.FolderLevel1 + "\\Database";
        var destinationFolder = destination + "\\Features\\" + objectToMap.FolderLevel1 + "\\Database";

        // create the desitonation folder
        Directory.CreateDirectory(destinationFolder);

        // copy the file with *.cs extension from source to destination
        foreach (string dirPath in Directory.GetFiles(sourceFolder, "*.sql", SearchOption.TopDirectoryOnly))
        {
            Console.WriteLine(dirPath);
            Console.WriteLine(dirPath.Replace(sourceFolder, destinationFolder));

            File.Copy(dirPath, dirPath.Replace(sourceFolder, destinationFolder), true);
        }
    }

    foreach (var objectToMap in objectsToMap)
    {
        var sourceFolder = source + "\\" + objectToMap.FolderLevel1 + "\\Tests";
        var destinationFolder = destination + "\\Features\\" + objectToMap.FolderLevel1 + "\\Tests";

        // create the desitonation folder
        Directory.CreateDirectory(destinationFolder);

        // copy the file with *.cs extension from source to destination
        foreach (string dirPath in Directory.GetFiles(sourceFolder, "*.http", SearchOption.TopDirectoryOnly))
        {
            Console.WriteLine(dirPath);
            Console.WriteLine(dirPath.Replace(sourceFolder, destinationFolder));

            File.Copy(dirPath, dirPath.Replace(sourceFolder, destinationFolder), true);
        }
    }

    foreach (var objectToMap in objectsToMap)
    {
        var sourceFolder = source + "\\Infrastructure";
        var destinationFolder = destination + "\\Infrastructure";

        // create the desitonation folder
        Directory.CreateDirectory(destinationFolder);

        // copy the file with *.cs extension from source to destination
        foreach (string dirPath in Directory.GetFiles(sourceFolder, "*.cs", SearchOption.TopDirectoryOnly))
        {
            Console.WriteLine(dirPath);
            Console.WriteLine(dirPath.Replace(sourceFolder, destinationFolder));

            File.Copy(dirPath, dirPath.Replace(sourceFolder, destinationFolder), true);
        }
    }
}

if (generateFrontend)
{
    // copy all files from the generated folder to the project folder
    var source = @"D:\Projects\crud\dbcreator\generated\features\config\Models";
    var destination = @"D:\Projects\crud\CrudUi\CrudUi\Pages\Features\Config\Models";

    // copy the file with *.cs extension from source to destination
    foreach (string dirPath in Directory.GetFiles(source, "*.cs", SearchOption.TopDirectoryOnly))
    {
        Console.WriteLine(dirPath);
        Console.WriteLine(dirPath.Replace(source, destination));

        File.Copy(dirPath, dirPath.Replace(source, destination), true);
    }

    source = @"D:\Projects\crud\dbcreator\generated\features\config\Services";
    destination = @"D:\Projects\crud\CrudUi\CrudUi\Pages\Features\Config\Services";

    // copy the file with *.cs extension from source to destination
    foreach (string dirPath in Directory.GetFiles(source, "*.cs", SearchOption.TopDirectoryOnly))
    {
        Console.WriteLine(dirPath);
        Console.WriteLine(dirPath.Replace(source, destination));

        File.Copy(dirPath, dirPath.Replace(source, destination), true);
    }

    source = @"D:\Projects\crud\dbcreator\generated\features\config\Pages\Editors";
    destination = @"D:\Projects\crud\CrudUi\CrudUi\Pages\Features\Config\Pages\Editors";

    // copy the file with *.cs extension from source to destination
    foreach (string dirPath in Directory.GetFiles(source, "*.*", SearchOption.TopDirectoryOnly))
    {
        Console.WriteLine(dirPath);
        Console.WriteLine(dirPath.Replace(source, destination));

        File.Copy(dirPath, dirPath.Replace(source, destination), true);
    }

     destination = @"D:\Projects\crud\CrudUi\CrudUi\Program.cs";
     var serviceDeclarationString = converter.GenerateFrontEndServiceDeclarationString(objectsToMap);


    var programFileText = File.ReadAllText(destination);
    var replaceLocationStart = programFileText.IndexOf("// *** SERVICES START ***") + "// *** SERVICES START ***".Length;
    var replaceLocationEnd = programFileText.IndexOf("// *** SERVICES END ***");

    programFileText = programFileText.Remove(replaceLocationStart, replaceLocationEnd - replaceLocationStart);

    programFileText = programFileText.Replace("// *** SERVICES START ***", "// *** SERVICES START ***\r\n" + serviceDeclarationString);

    File.WriteAllText(destination, programFileText);
}



if (generateNavigation)
{
    var navFolder = "generated/Features/Config/Pages/Navigation";
    Directory.CreateDirectory(navFolder);

    Navigation.GenerateNavigationFiles(navFolder);


    // copy all files from the generated folder to the project folder
    var source = @"D:\Projects\crud\dbcreator\generated\features\config\Pages\Navigation";
    var destination = @"D:\Projects\crud\CrudUi\CrudUi\Pages\Features\Config\Pages\Navigation";

    // copy the file with *.cs extension from source to destination
    foreach (string dirPath in Directory.GetFiles(source, "*.*", SearchOption.TopDirectoryOnly))
    {
        Console.WriteLine(dirPath);
        Console.WriteLine(dirPath.Replace(source, destination));

        File.Copy(dirPath, dirPath.Replace(source, destination), true);
    }
}


if (cleanUpFiles)
{
    Directory.Delete("generated", true);
}