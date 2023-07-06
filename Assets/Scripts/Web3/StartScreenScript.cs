using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using System.Threading.Tasks;
using TMPro;

public class StartScreenScript : MonoBehaviour
{

    private ThirdwebSDK sdk;
    private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _tokenBalance;
    [SerializeField] private GameObject _claimButton;
    [SerializeField] private GameObject _claimingTextObject;
    [SerializeField] private TextMeshProUGUI _claimingText;

    private const string DROP_ERC20_CONTRACT = "0xb1917318ae18B09D85eeC61f13aC315Bb7726D63";

    void Start()
    {
        sdk = new ThirdwebSDK("goerli");
        _gameManager = GameManager.instance;
    }

    public async void toggleStartScreen(GameObject ConnectedState, GameObject DisconnectedState, string address)
    {
        ConnectedState.SetActive(true);
        DisconnectedState.SetActive(false);

        string stringBalance = await checkTokenBalance(address);

        _tokenBalance.text = $"BIRB tokens: {stringBalance}";
    }

    public void untoggleStartScreen(GameObject ConnectedState, GameObject DisconnectedState)
    {
        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
    }

    public async Task<string> checkTokenBalance(string address)
    {
        Contract contract= sdk.GetContract(DROP_ERC20_CONTRACT);

        string balanceString = await contract.Read<string>("balanceOf", address);

        return balanceString.ToEth(18, false);
    }

    public async void claimTokens()
    {
        Contract contract = sdk.GetContract(DROP_ERC20_CONTRACT);
        int tokenQty = _gameManager.CurrentScore;

        _claimButton.SetActive(false);
        _claimingTextObject.SetActive(true);

        var result = await contract.ERC20.Claim(tokenQty.ToString());
        if (result.isSuccessful())
        {
            _claimingText.text = "Tokens claimed successfully!";
        }
    }
}
