import {
  addWindowClass,
  calculateWindowSize,
  removeWindowClass
} from '@app/utils/helpers';
import {
  NAVBAR_DARK_VARIANTS,
  NAVBAR_LIGHT_VARIANTS,
  SIDEBAR_DARK_SKINS,
  SIDEBAR_LIGHT_SKINS
} from '@app/utils/themes';
import { createReducer } from '@reduxjs/toolkit';
import { setNavbarVariant, setSidebarSkin, setWindowSize, toggleControlSidebar, toggleDarkMode, toggleFooterFixed, toggleHeaderBorder, toggleHeaderFixed, toggleLayoutBoxed, toggleLayoutFixed, toggleMenuChildIndent, toggleMenuItemFlat, toggleSidebarMenu } from './actions';
import { UiState } from './state';

const initialState: UiState = {
  screenSize: calculateWindowSize(window.innerWidth),
  darkMode: false,
  navbarVariant: 'navbar-light',
  sidebarSkin: 'sidebar-dark-primary',
  menuSidebarCollapsed: false,
  controlSidebarCollapsed: true,
  headerBorder: false,
  headerFixed: false,
  footerFixed: true,
  layoutBoxed: false,
  menuItemFlat: false,
  menuChildIndent: false,
  layoutFixed: false
};

addWindowClass('layout-footer-fixed');

// Export the entity reducer
export const uiReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(toggleSidebarMenu, (state) => {
      state.menuSidebarCollapsed = !state.menuSidebarCollapsed;
    })
    .addCase(toggleControlSidebar, (state) => {
      state.controlSidebarCollapsed = !state.controlSidebarCollapsed;
    })
    .addCase(toggleHeaderBorder, (state) => {
      state.controlSidebarCollapsed = !state.controlSidebarCollapsed;
    })
    .addCase(toggleHeaderFixed, (state) => {
      state.headerFixed = !state.headerFixed;
      if (state.headerFixed) {
        addWindowClass('layout-navbar-fixed');
      } else {
        removeWindowClass('layout-navbar-fixed');
      }
    })
    .addCase(toggleFooterFixed, (state) => {
      state.footerFixed = !state.footerFixed;
      if (state.footerFixed) {
        addWindowClass('layout-footer-fixed');
      } else {
        removeWindowClass('layout-footer-fixed');
      }
    })
    .addCase(toggleLayoutBoxed, (state) => {
      state.layoutBoxed = !state.layoutBoxed;
      if (state.layoutBoxed) {
        addWindowClass('layout-boxed');
      } else {
        removeWindowClass('layout-boxed');
      }
    })
    .addCase(toggleLayoutFixed, (state) => {
      state.layoutFixed = !state.layoutFixed;
      if (state.layoutFixed) {
        removeWindowClass('layout-fixed');
      } else {
        addWindowClass('layout-fixed');
      }
    })
    .addCase(toggleMenuItemFlat, (state) => {
      state.menuItemFlat = !state.menuItemFlat;
    })
    .addCase(toggleMenuChildIndent, (state) => {
      state.menuChildIndent = !state.menuChildIndent;
    })
    .addCase(toggleDarkMode, (state) => {
      state.darkMode = !state.darkMode;
      if (state.darkMode) {
        state.navbarVariant = NAVBAR_DARK_VARIANTS[0].value;
        state.sidebarSkin = SIDEBAR_DARK_SKINS[0].value;
      } else {
        state.navbarVariant = NAVBAR_LIGHT_VARIANTS[0].value;
        state.sidebarSkin = SIDEBAR_LIGHT_SKINS[0].value;
      }
      if (state.darkMode) {
        addWindowClass('dark-mode');
      } else {
        removeWindowClass('dark-mode');
      }
    })
    .addCase(setNavbarVariant, (state, {payload}) => {
      if (state.darkMode) {
        state.navbarVariant = payload || NAVBAR_DARK_VARIANTS[0].value;
      } else {
        state.navbarVariant = payload || NAVBAR_LIGHT_VARIANTS[0].value;
      }
    })
    .addCase(setSidebarSkin, (state, {payload}) => {
      if (state.darkMode) {
        state.sidebarSkin = payload || SIDEBAR_DARK_SKINS[0].value;
      } else {
        state.sidebarSkin = payload || SIDEBAR_LIGHT_SKINS[0].value;
      }
    })
    .addCase(setWindowSize, (state, {payload}) => {
      state.screenSize = payload;
    });
});
