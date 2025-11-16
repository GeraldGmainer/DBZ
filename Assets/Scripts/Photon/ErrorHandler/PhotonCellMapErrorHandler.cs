using UnityEngine;
using UnityEngine.Events;

public class PhotonCellMapErrorHandler : PhotonGenericErrorHandler {
    [SerializeField]
    private RectTransform cellMapMenuHandlerPanel;

    //private CellArenaMenuHandler cellArenaMenuHandler;

    void Awake() {
        //cellArenaMenuHandler = cellMapMenuHandlerPanel.GetComponent<CellArenaMenuHandler>();
    }

    private void goToCellMapMenu() {
    }

    public override UnityAction getButtonAction() {
        return goToCellMapMenu;
    }
}
