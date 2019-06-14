﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public enum WindowsMessage
    {
        WM_NULL,
        WM_CREATE,
        WM_DESTROY,
        WM_MOVE,
        WM_SIZE = 5,
        WM_ACTIVATE,
        WM_SETFOCUS,
        WM_KILLFOCUS,
        WM_ENABLE = 10,
        WM_SETREDRAW,
        WM_SETTEXT,
        WM_GETTEXT,
        WM_GETTEXTLENGTH,
        WM_PAINT,
        WM_CLOSE,
        WM_QUERYENDSESSION,
        WM_QUERYOPEN = 19,
        WM_ENDSESSION = 22,
        WM_QUIT = 18,
        WM_ERASEBKGND = 20,
        WM_SYSCOLORCHANGE,
        WM_SHOWWINDOW = 24,
        WM_WININICHANGE = 26,
        WM_SETTINGCHANGE = 26,
        WM_DEVMODECHANGE,
        WM_ACTIVATEAPP,
        WM_FONTCHANGE,
        WM_TIMECHANGE,
        WM_CANCELMODE,
        WM_SETCURSOR,
        WM_MOUSEACTIVATE,
        WM_CHILDACTIVATE,
        WM_QUEUESYNC,
        WM_GETMINMAXINFO,
        WM_PAINTICON = 38,
        WM_ICONERASEBKGND,
        WM_NEXTDLGCTL,
        WM_SPOOLERSTATUS = 42,
        WM_DRAWITEM,
        WM_MEASUREITEM,
        WM_DELETEITEM,
        WM_VKEYTOITEM,
        WM_CHARTOITEM,
        WM_SETFONT,
        WM_GETFONT,
        WM_SETHOTKEY,
        WM_GETHOTKEY,
        WM_QUERYDRAGICON = 55,
        WM_COMPAREITEM = 57,
        WM_GETOBJECT = 61,
        WM_COMPACTING = 65,
        WM_COMMNOTIFY = 68,
        WM_WINDOWPOSCHANGING = 70,
        WM_WINDOWPOSCHANGED,
        WM_POWER,
        WM_COPYDATA = 74,
        WM_CANCELJOURNAL,
        WM_NOTIFY = 78,
        WM_INPUTLANGCHANGEREQUEST = 80,
        WM_INPUTLANGCHANGE,
        WM_TCARD,
        WM_HELP,
        WM_USERCHANGED,
        WM_NOTIFYFORMAT,
        WM_CONTEXTMENU = 123,
        WM_STYLECHANGING,
        WM_STYLECHANGED,
        WM_DISPLAYCHANGE,
        WM_GETICON,
        WM_SETICON,
        WM_NCCREATE,
        WM_NCDESTROY,
        WM_NCCALCSIZE,
        WM_NCHITTEST,
        WM_NCPAINT,
        WM_NCACTIVATE,
        WM_GETDLGCODE,
        WM_SYNCPAINT,
        WM_NCMOUSEMOVE = 160,
        WM_NCLBUTTONDOWN,
        WM_NCLBUTTONUP,
        WM_NCLBUTTONDBLCLK,
        WM_NCRBUTTONDOWN,
        WM_NCRBUTTONUP,
        WM_NCRBUTTONDBLCLK,
        WM_NCMBUTTONDOWN,
        WM_NCMBUTTONUP,
        WM_NCMBUTTONDBLCLK,
        WM_NCXBUTTONDOWN = 171,
        WM_NCXBUTTONUP,
        WM_NCXBUTTONDBLCLK,
        WM_INPUT_DEVICE_CHANGE = 254,
        WM_INPUT,
        WM_KEYFIRST,
        WM_KEYDOWN = 256,
        WM_KEYUP,
        WM_CHAR,
        WM_DEADCHAR,
        WM_SYSKEYDOWN,
        WM_SYSKEYUP,
        WM_SYSCHAR,
        WM_SYSDEADCHAR,
        WM_UNICHAR = 265,
        WM_KEYLAST = 265,
        WM_IME_STARTCOMPOSITION = 269,
        WM_IME_ENDCOMPOSITION,
        WM_IME_COMPOSITION,
        WM_IME_KEYLAST = 271,
        WM_INITDIALOG,
        WM_COMMAND,
        WM_SYSCOMMAND,
        WM_TIMER,
        WM_HSCROLL,
        WM_VSCROLL,
        WM_INITMENU,
        WM_INITMENUPOPUP,
        WM_MENUSELECT = 287,
        WM_MENUCHAR,
        WM_ENTERIDLE,
        WM_MENURBUTTONUP,
        WM_MENUDRAG,
        WM_MENUGETOBJECT,
        WM_UNINITMENUPOPUP,
        WM_MENUCOMMAND,
        WM_CHANGEUISTATE,
        WM_UPDATEUISTATE,
        WM_QUERYUISTATE,
        WM_CTLCOLORMSGBOX = 306,
        WM_CTLCOLOREDIT,
        WM_CTLCOLORLISTBOX,
        WM_CTLCOLORBTN,
        WM_CTLCOLORDLG,
        WM_CTLCOLORSCROLLBAR,
        WM_CTLCOLORSTATIC,
        MN_GETHMENU = 481,
        WM_MOUSEFIRST = 512,
        WM_MOUSEMOVE = 512,
        WM_LBUTTONDOWN,
        WM_LBUTTONUP,
        WM_LBUTTONDBLCLK,
        WM_RBUTTONDOWN,
        WM_RBUTTONUP,
        WM_RBUTTONDBLCLK,
        WM_MBUTTONDOWN,
        WM_MBUTTONUP,
        WM_MBUTTONDBLCLK,
        WM_MOUSEWHEEL,
        WM_XBUTTONDOWN,
        WM_XBUTTONUP,
        WM_XBUTTONDBLCLK,
        WM_MOUSEHWHEEL,
        WM_PARENTNOTIFY = 528,
        WM_ENTERMENULOOP,
        WM_EXITMENULOOP,
        WM_NEXTMENU,
        WM_SIZING,
        WM_CAPTURECHANGED,
        WM_MOVING,
        WM_POWERBROADCAST = 536,
        WM_DEVICECHANGE,
        WM_MDICREATE = 544,
        WM_MDIDESTROY,
        WM_MDIACTIVATE,
        WM_MDIRESTORE,
        WM_MDINEXT,
        WM_MDIMAXIMIZE,
        WM_MDITILE,
        WM_MDICASCADE,
        WM_MDIICONARRANGE,
        WM_MDIGETACTIVE,
        WM_MDISETMENU = 560,
        WM_ENTERSIZEMOVE,
        WM_EXITSIZEMOVE,
        WM_DROPFILES,
        WM_MDIREFRESHMENU,
        WM_IME_SETCONTEXT = 641,
        WM_IME_NOTIFY,
        WM_IME_CONTROL,
        WM_IME_COMPOSITIONFULL,
        WM_IME_SELECT,
        WM_IME_CHAR,
        WM_IME_REQUEST = 648,
        WM_IME_KEYDOWN = 656,
        WM_IME_KEYUP,
        WM_MOUSEHOVER = 673,
        WM_MOUSELEAVE = 675,
        WM_NCMOUSEHOVER = 672,
        WM_NCMOUSELEAVE = 674,
        WM_WTSSESSION_CHANGE = 689,
        WM_TABLET_FIRST = 704,
        WM_TABLET_LAST = 735,
        WM_CUT = 768,
        WM_COPY,
        WM_PASTE,
        WM_CLEAR,
        WM_UNDO,
        WM_RENDERFORMAT,
        WM_RENDERALLFORMATS,
        WM_DESTROYCLIPBOARD,
        WM_DRAWCLIPBOARD,
        WM_PAINTCLIPBOARD,
        WM_VSCROLLCLIPBOARD,
        WM_SIZECLIPBOARD,
        WM_ASKCBFORMATNAME,
        WM_CHANGECBCHAIN,
        WM_HSCROLLCLIPBOARD,
        WM_QUERYNEWPALETTE,
        WM_PALETTEISCHANGING,
        WM_PALETTECHANGED,
        WM_HOTKEY,
        WM_PRINT = 791,
        WM_PRINTCLIENT,
        WM_APPCOMMAND,
        WM_THEMECHANGED,
        WM_CLIPBOARDUPDATE = 797,
        WM_DWMCOMPOSITIONCHANGED,
        WM_DWMNCRENDERINGCHANGED,
        WM_DWMCOLORIZATIONCOLORCHANGED,
        WM_DWMWINDOWMAXIMIZEDCHANGE,
        WM_GETTITLEBARINFOEX = 831,
        WM_HANDHELDFIRST = 856,
        WM_HANDHELDLAST = 863,
        WM_AFXFIRST,
        WM_AFXLAST = 895,
        WM_PENWINFIRST,
        WM_PENWINLAST = 911,
        WM_APP = 32768,
        WM_USER = 1024,
        WM_REFLECT = 8192
    }

}