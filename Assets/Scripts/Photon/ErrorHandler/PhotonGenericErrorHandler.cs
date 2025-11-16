using UnityEngine.Events;
using Photon;

public abstract class PhotonGenericErrorHandler : PunBehaviour {
    public abstract UnityAction getButtonAction();

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg) {
        if (getErrorCode(codeAndMsg) == "32766") {
            ErrorDialog.Instance.show("SERVER ERROR", "Server Name Already Exists", getButtonAction());
        }
        else {
            ErrorDialog.Instance.show("SERVER ERROR", "Unknown Error: " + codeAndMsg[0] + "\n" + codeAndMsg[1], getButtonAction());
        }
    }

    public override void OnConnectionFail(DisconnectCause cause) {
        ErrorDialog.Instance.show("CONNECTION ERROR", "Connection Fail:\n" + cause, getButtonAction());
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause) {
        if (cause == DisconnectCause.ExceptionOnConnect) {
            ErrorDialog.Instance.show("CONNECTION ERROR", "Cannot Connect To The Server", getButtonAction());
        }
        else if (cause == DisconnectCause.DisconnectByClientTimeout) {
            ErrorDialog.Instance.show("CONNECTION ERROR", "Timeout", getButtonAction());
        }
        else if (cause == DisconnectCause.MaxCcuReached) {
            ErrorDialog.Instance.show("CONNECTION ERROR", "Maximum Connection Limit Reached", getButtonAction());
        }
        else {
            ErrorDialog.Instance.show("CONNECTION ERROR", "Failed To Connect:\n" + cause, getButtonAction());
        }
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
        base.OnPhotonJoinRoomFailed(codeAndMsg);
        if (getErrorCode(codeAndMsg) == "32765") {
            ErrorDialog.Instance.show("JOIN ERROR", "Server Is Full", getButtonAction());
        }
        else {
            ErrorDialog.Instance.show("JOIN ERROR", "Join Server failed: " + codeAndMsg[0] + "\n" + codeAndMsg[1], getButtonAction());
        }
    }

    private string getErrorCode(object[] codeAndMsg) {
        return codeAndMsg[0].ToString();
    }
}
