import { APP_INITIALIZER } from '@angular/core';
import { StartupService } from '../services/startup.service';

// import { SanctumService } from './bootstrap/sanctum.service';
// export function SanctumServiceFactory(sanctumService: SanctumService) {
//   return () => sanctumService.load();
// }

// import { TranslateLangService } from './bootstrap/translate-lang.service';
// export function TranslateLangServiceFactory(translateLangService: TranslateLangService) {
//   return () => translateLangService.load();
// }


export function StartupServiceFactory(startupService: StartupService) {
  return () => startupService.load();
}

export const appInitializerProviders = [
  // {
  //   provide: APP_INITIALIZER,
  //   useFactory: SanctumServiceFactory,
  //   deps: [SanctumService],
  //   multi: true,
  // },
//   {
//     provide: APP_INITIALIZER,
//     useFactory: TranslateLangServiceFactory,
//     deps: [TranslateLangService],
//     multi: true,
//   },
  {
    provide: APP_INITIALIZER,
    useFactory: StartupServiceFactory,
    deps: [StartupService],
    multi: true,
  },
];
