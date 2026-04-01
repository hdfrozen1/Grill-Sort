# UI Game Setup Tutorial — Grill Sort Clone

This document provides detailed hierarchy structures and component properties for a professional UI setup in Unity or similar game engines. It uses **Persistent UI** and **CanvasGroup** techniques for smooth transitions.

---

## 0. Global Project Settings
| Property | Value |
|---|---|
| **Canvas Scaler** | Scale With Screen Size |
| **Reference Resolution** | 720 × 1560 |
| **Screen Match Mode** | Match Width or Height (0.5) |
| **Font** | Rounded Bold (e.g., Fredoka One) |

---

## 1. Global Hierarchy Architecture
To achieve the "seamless" feel, organize your Root UI as follows:
```text
[Root UI]
├── Canvas_Background (Static sky/city image)
├── Canvas_Main_Content (Contextual screens)
│   ├── UI_Home (CanvasGroup)
│   ├── UI_Shop (CanvasGroup)
│   └── UI_Win (CanvasGroup)
├── Canvas_Persistent (Always visible)
│   ├── TopBar (Lives, Coins, Settings Btn)
│   └── BottomNav (Tab Bar)
└── Canvas_Overlay (Modal popups)
    ├── Black_Dim (Raycast Target: True)
    ├── Popup_Settings
    ├── Popup_Lose
    └── Popup_OutOfSlot (Fail Offer)
```

---

## 2. Home Screen (`home.png`)
### Components
- **Game Scene:** Non-interactive background with the food truck and characters [cite: 3, 10].
- **Side Stack (Left):** Vertical group containing Star Chest (with progress slider), Gallery, and Remove Ads buttons [cite: 3, 10].
- **Play Button:** Large green button with "Play" and "Level 6" text, anchored Bottom-Center [cite: 3, 10].

### Coplay Setup
```
1. Under Canvas_Main_Content -> UI_Home (Add CanvasGroup component).
2. Create LeftButtons: VerticalLayoutGroup (Spacing: 20, Anchor: Middle-Left).
   - Item: StarChestBtn (Image + Child Slider: 223/500).
   - Item: GalleryBtn (Image + Text).
   - Item: RemoveAdsBtn (Image + Text).
3. Create PlayGroup: Anchored Bottom-Center (Pos Y: 300).
   - BtnPlay: Green 9-slice Sprite.
   - ProgressRow: Two horizontal sliders for level tracking.
```

---

## 3. Shop Screen (`shop.png`)
### Components
- **Header:** Striped red/white awning background [cite: 1, 5, 10].
- **Scroll Area:** Vertical ScrollView containing item bundles [cite: 1, 5, 10].
- **Pack Panel:** Beige background with items (coins, powerups, timers) and a green price button [cite: 1, 5, 10].

### Coplay Setup
```
1. Under Canvas_Main_Content -> UI_Shop (Add CanvasGroup, Alpha 0 by default).
2. Add ScrollView:
   - Content: VerticalLayoutGroup + ContentSizeFitter (Vertical Fit).
   - ItemTemplate: 9-slice rounded panel.
     - Top: Pack Name (e.g., "Starter Pack").
     - Center: HorizontalLayoutGroup for reward icons.
     - Bottom: BtnPurchase (Green, Price text).
```

---

## 4. Win Screen - "Well Done" (`win.png`)
### Components
- **Banner:** Red ribbon at the top with "Well Done" text [cite: 9].
- **Multiplier Slider:** Horizontal bar with x2, x3, x5 segments and a moving indicator [cite: 9].
- **Claim Buttons:** Large Blue (Claim with Ads) and Green (Continue) buttons [cite: 9].

### Coplay Setup
```
1. Under Canvas_Main_Content -> UI_Win (Add CanvasGroup).
2. TitleBanner: Anchor Top-Center.
3. MultiplierGroup: Horizontal slider with 5 colored Image segments.
4. ButtonGroup: VerticalLayoutGroup.
   - BtnClaim: Blue sprite + AD icon.
   - BtnContinue: Green sprite + Gold coin icon.
```

---

## 5. Lose Popup - "Level Failed" (`lose.png`)
### Components
- **Popup Base:** White panel with orange border, "Level Failed" gray banner [cite: 6].
- **Status:** Large Heart icon with "-1" text [cite: 6].
- **Booster Selection:** Horizontal group of 3 brown slots (Locked/Unlocked) [cite: 6].
- **Buttons:** Green "Try Again" and Blue "Try Again With Booster" [cite: 6].

### Coplay Setup
```
1. Under Canvas_Overlay -> Popup_Lose.
2. TitleBanner: "Level Failed" gray rounded rect.
3. BoosterGroup: HorizontalLayoutGroup (Spacing: 15).
   - Slot: Brown square + Item icon + Lock icon child.
4. ActionGroup: HorizontalLayoutGroup.
   - BtnRetry: Green rounded rect.
   - BtnRetryAD: Blue rounded rect + AD icon.
```

---

## 6. Out Of Slot / Fail Offer (`lose_notifi.png`)
### Components
- **Main Message:** "You will lose" with Heart/Star icons [cite: 7].
- **Free Button:** Large green button in the center [cite: 7, 8].
- **Fail Offer Panel:** Special pink/beige footer panel with multiple items and an IAP price button [cite: 7, 8].

### Coplay Setup
```
1. Under Canvas_Overlay -> Popup_OutOfSlot.
2. UpperPanel: White rounded panel showing penalties.
3. OfferPanel (Bottom):
   - Image: Beige 9-slice with Pink title bar "Fail Offer".
   - HorizontalLayoutGroup: Coins, Snowflakes, Magnet, Arrow, Star icons.
   - BtnPurchase: Green button (26.000 đ) anchored right.
4. Pagination: Two white dots indicating more offers.
```

---

## 7. Persistent UI (Header & Footer)
### Header Setup
- **LivesDisplay:** Center-Top, Pill-shaped image, Text "5 Full" [cite: 3, 10].
- **CoinsDisplay:** Top-Right, Pill-shaped image, Text "150" [cite: 3, 5, 10].
- **SettingsBtn:** Top-Left, Square button, Gear icon [cite: 3, 10].

### Footer (Tab Bar) Setup
- **Layout:** HorizontalLayoutGroup (3 tabs) [cite: 3, 5, 10].
- **TabShop:** Shop icon + Text [cite: 3, 5, 10].
- **TabHome:** Truck icon (Raised position for active state) [cite: 3, 5, 10].
- **TabLeaderboard:** Lock icon [cite: 3, 5, 10].

---

## Implementation Tips for Coplay:
1. **Raycast Targets:** Ensure `Black_Dim` has Raycast Target = **True** to block clicks on the Home screen when a popup is open.
2. **Canvas Group Fading:** Use a script to lerp `CanvasGroup.alpha` from 0 to 1 over 0.2s for the "fast/smooth" transition feel.
3. **9-Slicing:** Use 9-slice sprites for all rounded buttons and panels to maintain crisp corners at any size.
