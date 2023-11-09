import { createAction } from '@reduxjs/toolkit';

export const toggleSidebarMenu = createAction('ui/toggleSidebarMenu');
export const toggleControlSidebar = createAction('ui/toggleControlSidebar');
export const toggleDarkMode = createAction('ui/toggleDarkMode');
export const toggleHeaderBorder = createAction('ui/toggleHeaderBorder');
export const toggleHeaderFixed = createAction('ui/toggleHeaderFixed');
export const toggleFooterFixed = createAction('ui/toggleFooterFixed');
export const toggleLayoutBoxed = createAction('ui/toggleLayoutBoxed');
export const toggleMenuItemFlat = createAction('ui/toggleMenuItemFlat');
export const toggleMenuChildIndent = createAction('ui/toggleMenuChildIndent');
export const toggleLayoutFixed = createAction('ui/toggleLayoutFixed');
export const setNavbarVariant = createAction<string>('ui/setNavbarVariant');
export const setSidebarSkin = createAction<string>('ui/setSidebarSkin');
export const setWindowSize = createAction<string>('ui/setWindowSize');
