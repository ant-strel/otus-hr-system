import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { Route, RouterProvider, Routes } from 'react-router';
import Enterform from './Components/EnterComponent/main-form-component/EnterForm';
import { createBrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './store';
import { Fragment } from 'react';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const router=createBrowserRouter([
  {
    path:'/',
    element:<App/>
  },
  {
    path:'/auth/',
    element:<Enterform/>
  }
]);

root.render(
<Provider store={store}>
<Fragment>
      <RouterProvider router={router}/>
</Fragment>
</Provider>);

reportWebVitals();
