using System;
using System.Collections;
using System.Threading.Tasks;
using MobileClientXamarin;
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using MClient;
using ThreadViewHelper;

/// <summary>
/// Аналог класса Client в unity для связи UI и логики
/// </summary>
public class Client
{
    private string _playerName; //"Pot Player" or "YouTube Player" 

    private EditText _ipAddressForServer; // Для ввода ip адреса компьютера?
    private TextView _playerNameText; // Выводит название выбранного плеера(PotPlayer)

    // private FileList uiFileList; // для вывода информации JSON файла структуры файловой системы компьютера

    private static MobileClient _mobileCommandSender;

    public static MobileClient MobileCommandSender => _mobileCommandSender;
    //private MobileServer mobileServer;


    public Client(EditText ipAddressForServer, TextView playerNameText)
    {
        _ipAddressForServer = ipAddressForServer;
        _playerNameText = playerNameText;
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        ThreadViewer.SetPrinter(new XamarinPrinter());
        SwitchPlayerName("Pot Player");
        // LogMessageList myMessageList = FindObjectOfType<LogMessageList>();
        // ConsoleInTextView.Init(myMessageList);
        _ipAddressForServer.Text = "192.168.0.101";

        _mobileCommandSender = new MobileClient(_ipAddressForServer.Text);
    }

    public async void StartServer()
    {
        // StartCoroutine(StartServerCorut());
        await StartServerAsync();
    }

    private async Task StartServerAsync()
    {
        ConsoleInTextView.LogInText("--->StartServer Start");

        // Отсылаем запрос на получение фавйловой системы в Json формате
        SendGetFileSystem("4");

        ConsoleInTextView.LogInText("Выслан запрос на JSON.");
        var data = await MobileServer.AwaitMessageAsync();

        // uiFileList.gameObject.SetActive(true);

        // Выводим Json в UI
        ConsoleInTextView.LogInText("Выводим Json в UI.");
        // var sb = new ServerBrowser(uiFileList);

        ConsoleInTextView.LogInText("Построение UI.");
        //  await sb.ShowInBrowser(data);
        ConsoleInTextView.LogInText("Построение UI завершено.");

        ConsoleInTextView.LogInText("--->StartServer End");
    }

    public void SendGetFileSystem(string deep)
    {
        if (string.IsNullOrEmpty(deep)) { deep = "1"; }

        MobileCommandSender.SendCommand("GetFileSystem", deep);
    }

    public void SendRight10()
    {
        MobileCommandSender.SendCommand("Right x 10", _playerName);
    }

    public void SendRight()
    {
        MobileCommandSender.SendCommand("Right", _playerName);
    }
    public void SendRight(string count)
    {
        MobileCommandSender.SendCommand("Right", _playerName + "&" + count);
    }
    public void SendLeft(string count)
    {
        MobileCommandSender.SendCommand("Left", _playerName + "&" + count);
    }

    public void SendLeft10()
    {
        MobileCommandSender.SendCommand("Left x 10", _playerName);
    }

    public void SendLeft()
    {
        MobileCommandSender.SendCommand("Left", _playerName);
    }

    public void SendVolumeH()
    {
        MobileCommandSender.SendCommand("Volume +", _playerName);
    }

    public void SendVolumeL()
    {
        MobileCommandSender.SendCommand("Volume -", _playerName);
    }

    public void SendVolumeMute()
    {
        MobileCommandSender.SendCommand("Mute", _playerName);
    }

    public void SendPageDown()
    {
        MobileCommandSender.SendCommand("PageDown", _playerName);
    }

    public void SendPageUp()
    {
        MobileCommandSender.SendCommand("PageUp", _playerName);
    }

    public void SendHibernate()
    {
        MobileCommandSender.SendCommand("Hibernate", _playerName);
    }

    public void SendStandBy()
    {
        MobileCommandSender.SendCommand("StandBy", _playerName);
    }

    /// <summary>
    /// Space используется по умолчанию на сервере
    /// </summary>
    public void SendSpace()
    {
        MobileCommandSender.SendCommand("Space", _playerName);
    }

    public void SendWol()
    {
        try { WakeOnLan.WakeFunction("001E58A044B5"); }
        catch (Exception e)
        {
            ConsoleInTextView.ShowError($"формат : 001E58A044B5  \n{e.Message}");
        }
    }

    public void SendSaveName()
    {
        MobileCommandSender.SendCommand("SaveName");
    }

    public void SendLoadMovie()
    {
        MobileCommandSender.SendCommand("LoadLastMovie");
    }

    public void SwitchPlayerName(string playerName)
    {
        _playerNameText.Text = playerName;
        _playerName = playerName;
    }
}
