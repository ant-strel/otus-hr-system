import { configureStore  } from '@reduxjs/toolkit';
import screenReducer from './StateManagement/screenReducer';
import popupReducer from './StateManagement/popupReducer';

const store= configureStore({
    reducer: {
        screen: screenReducer,
        popup: popupReducer
    },
   
});
export default store;

