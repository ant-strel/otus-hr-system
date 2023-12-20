interface PopupElem {
    popups: string[];
}

const initialState: PopupElem = {
    popups: [],
}

export const PopupState = Object.freeze({
    CLOSE_OPEN_POPUP: 'CLOSE_OPEN_POPUP_EVENT',
    CLOSE_POPUP: 'CLOSE_POPUP_EVENT',
});

type Action = { type: string, payload?: any };

const popupReducer = (state = initialState, action: Action) => {
    switch (action.type) {
        case PopupState.CLOSE_OPEN_POPUP:
            if(state.popups.findIndex((p:string)=> p == action.payload) >= 0){
                return { ...state, popups: state.popups.filter(x=>x != action.payload)}
            }else{
                return { ...state, popups: [...state.popups,action.payload]};
            }
        case PopupState.CLOSE_POPUP:
                return { ...state, popups: []};
        default:
            return state;
    }
}


export default popupReducer;