using ExitGames.Client.Photon;

public class PhotonCharacterInitializer : Photon.MonoBehaviour {

    public static void init(string name) {
        PhotonNetwork.player.name = name;

        Hashtable PlayerCustomProps = new Hashtable();
        PlayerCustomProps["ping"] = PhotonNetwork.GetPing();
        PlayerCustomProps["deaths"] = 0;
        PlayerCustomProps["kills"] = 0;
        PhotonNetwork.player.SetCustomProperties(PlayerCustomProps);
    }
}
