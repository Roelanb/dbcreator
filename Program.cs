//     var connectionString = "server=.;database=MES;Persist Security Info=False;Integrated Security=True;TrustServerCertificate=True;";            

// var test = ObjectsToMapData.QualityCategories(connectionString);

// test.LoadColumnsFromSqlCreateCommand();

// return;

var generateBackend = true;
var generateFrontend = true;
var generateNavigation = true;

var cleanUpFiles = true;

var connectionString = "server=bebodbst3;database=MES;Persist Security Info=False;Integrated Security=True;TrustServerCertificate=True;";            
var uiNameSpace = "MesExplorer";


var generatedFolder = $"{Directory.GetCurrentDirectory()}" + @"\generated";

var testsFolder = $"{Directory.GetCurrentDirectory()}" + @"\tests";

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine($"Generating files in {generatedFolder}");
Console.ResetColor();

var converter = new Converter();

var objectsToMap = ObjectsToMapData.GetObjectsToMap(connectionString);



var infrastructureApiFileText = converter.GenerateApiInfrastructureString(objectsToMap);

var infrastructureFolder = "generated/Infrastructure";
Directory.CreateDirectory(infrastructureFolder);
File.WriteAllText(infrastructureFolder + "/ServiceCollectionExtensions.cs", infrastructureApiFileText);

foreach (var objectToMap in objectsToMap)
{
    var fs = FileHelpers.CreateFolders("generated", objectToMap);
    
    if (generateBackend)
    {
        var entityFileText = converter.GenerateEntityCSharpClass(objectToMap);

        File.WriteAllText(fs.entitiesFolder + "/" + objectToMap.EntityName + ".cs", entityFileText);

        var repoFileText = converter.GenerateRepositoryClassString(objectToMap);

        File.WriteAllText(fs.sharedFolder + "/" + objectToMap.Name + "Repository.cs", repoFileText);

        var iRepoFileText = converter.GenerateIRepositoryClassString(objectToMap);

        File.WriteAllText(fs.sharedFolder + "/I" + objectToMap.Name + "Repository.cs", iRepoFileText);

        var queryFileText = converter.GenerateQueryClassString(objectToMap);

        File.WriteAllText(fs.queriesFolder + "/" + objectToMap.Name + "Query.cs", queryFileText);

        var controllerFileText = converter.GenerateControllerClassString(objectToMap);

        File.WriteAllText(fs.controllersFolder + "/V1/" + objectToMap.Name + "Controller.cs", controllerFileText);

        var exampleRestCallsFileText = converter.GenerateExampleRestCallsString(objectToMap);

        File.WriteAllText(fs.testsFolder + "/" + objectToMap.Name + "RestCalls.http", exampleRestCallsFileText);

        converter.GenerateDatabaseObjects(fs.databaseFolder, objectToMap);


    }

    if (generateFrontend)
    {
        var modelFileText = converter.GenerateModelClassString(objectToMap);

        File.WriteAllText(fs.modelsFolder + "/" + objectToMap.EntityName + ".cs", modelFileText);

        var iserviceFileText = converter.GenerateIServicesClassString(objectToMap);

        File.WriteAllText(fs.servicesFolder + "/I" + objectToMap.Name + "Service.cs", iserviceFileText);

        var serviceFileText = converter.GenerateServicesClassString(objectToMap);

        File.WriteAllText(fs.servicesFolder + "/" + objectToMap.Name + "Service.cs", serviceFileText);

        var editorFileText = converter.GenerateEditorRazorClassString(objectToMap);

        File.WriteAllText(fs.editorsFolder + "/" + objectToMap.Name + "Editor.razor", editorFileText);

        var editorCodeFileText = converter.GenerateEditorClassString(objectToMap);

        File.WriteAllText(fs.editorsFolder + "/" + objectToMap.Name + "Editor.razor.cs", editorCodeFileText);




    }

}

if (generateBackend)
{
    // copy all files from the generated folder to the project folder
    var source = generatedFolder;
    var destination = @"C:\capdev\Repos\DataService\DataService\DataService.Config";

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
        var sourceTests = testsFolder;
    
        var sourceFolder = sourceTests + "\\" + objectToMap.FolderLevel1;
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
    var source = @$"{generatedFolder}\features\config\Models";
    var destination = @"C:\capdev\Repos\MesExplorer\MesExplorer\Features\Config\Models";

    // copy the file with *.cs extension from source to destination
    foreach (string dirPath in Directory.GetFiles(source, "*.cs", SearchOption.TopDirectoryOnly))
    {
        Console.WriteLine(dirPath);
        Console.WriteLine(dirPath.Replace(source, destination));

        File.Copy(dirPath, dirPath.Replace(source, destination), true);
    }

    source = @$"{generatedFolder}\features\config\Services";
    destination =  @"C:\capdev\Repos\MesExplorer\MesExplorer\Features\Config\Services";

    // copy the file with *.cs extension from source to destination
    foreach (string dirPath in Directory.GetFiles(source, "*.cs", SearchOption.TopDirectoryOnly))
    {
        Console.WriteLine(dirPath);
        Console.WriteLine(dirPath.Replace(source, destination));

        File.Copy(dirPath, dirPath.Replace(source, destination), true);
    }

    source = @$"{generatedFolder}\features\config\Pages\Editors";
    destination =  @"C:\capdev\Repos\MesExplorer\MesExplorer\Features\Config\Pages\Editors";

    // copy the file with *.cs extension from source to destination
    foreach (string dirPath in Directory.GetFiles(source, "*.*", SearchOption.TopDirectoryOnly))
    {
        Console.WriteLine(dirPath);
        Console.WriteLine(dirPath.Replace(source, destination));

        File.Copy(dirPath, dirPath.Replace(source, destination), true);
    }

     destination = @"C:\capdev\Repos\MesExplorer\MesExplorer\Program.cs";
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

    Navigation.GenerateNavigationFiles(uiNameSpace,navFolder);


    // copy all files from the generated folder to the project folder
    var source = @$"{generatedFolder}\features\config\Pages\Navigation";
    var destination = @"C:\capdev\Repos\MesExplorer\MesExplorer\Features\Config\Pages\Navigation";

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