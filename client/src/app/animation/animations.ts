import { animate, state, style, transition, trigger } from "@angular/animations";



// animations.ts
export const fadeInOut = trigger('fadeInOut', [
    state('void', style({
      opacity: 0
    })),
    transition('void <=> *', [
      animate(300, style({ opacity: 1 })),
      animate(300, style({ opacity: 0 })),
    ]),
  ]);