import { createSelector } from '@reduxjs/toolkit';
import { RootState } from '../store';

// Export the entity selectors
export const selectUi = (state: RootState) => state.ui;

export const selectUiScreenSize = createSelector(
  selectUi,
  (uiState) => uiState.screenSize
);
export const selectUiMenuSidebarCollapsed = createSelector(
  selectUi,
  (uiState) => uiState.menuSidebarCollapsed
);
export const selectUiControlSidebarCollapsed = createSelector(
  selectUi,
  (uiState) => uiState.controlSidebarCollapsed
);
export const selectUiDarkMode = createSelector(
  selectUi,
  (uiState) => uiState.darkMode
);
export const selectUiHeaderBorder = createSelector(
  selectUi,
  (uiState) => uiState.headerBorder
);
export const selectUiHeaderFixed = createSelector(
  selectUi,
  (uiState) => uiState.headerFixed
);
export const selectUiFooterFixed = createSelector(
  selectUi,
  (uiState) => uiState.footerFixed
);
export const selectUiLayoutBoxed = createSelector(
  selectUi,
  (uiState) => uiState.layoutBoxed
);
export const selectUiLayoutFixed = createSelector(
  selectUi,
  (uiState) => uiState.layoutFixed
);
export const selectUiMenuItemFlat = createSelector(
  selectUi,
  (uiState) => uiState.menuItemFlat
);
export const selectUiMenuChildIndent = createSelector(
  selectUi,
  (uiState) => uiState.menuChildIndent
);
export const selectUiNavbarVariant = createSelector(
  selectUi,
  (uiState) => uiState.navbarVariant
);
export const selectUiSidebarSkin = createSelector(
  selectUi,
  (uiState) => uiState.sidebarSkin
);
