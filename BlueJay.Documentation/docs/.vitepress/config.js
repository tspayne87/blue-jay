/**
 * @type {import('vitepress').UserConfig}
 */
const config = {
  title: 'BlueJay',
  head: [
    ['script', { type: 'module' }, `
    // Import the functions you need from the SDKs you need
    import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.3/firebase-app.js";
    import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.6.3/firebase-analytics.js";
    // TODO: Add SDKs for Firebase products that you want to use
    // https://firebase.google.com/docs/web/setup#available-libraries
  
    // Your web app's Firebase configuration
    // For Firebase JS SDK v7.20.0 and later, measurementId is optional
    const firebaseConfig = {
      apiKey: "AIzaSyDD7olm8Nx2_k8bYYpEo4y7381BdbAn-Eo",
      authDomain: "bluejay-ae0bf.firebaseapp.com",
      projectId: "bluejay-ae0bf",
      storageBucket: "bluejay-ae0bf.appspot.com",
      messagingSenderId: "324283472",
      appId: "1:324283472:web:831e22b6006b8f032c3555",
      measurementId: "G-8EDM2TS9K1"
    };
  
    // Initialize Firebase
    const app = initializeApp(firebaseConfig);
    const analytics = getAnalytics(app);
    `]
  ],
  description: 'Documentation for using the BlueJay Framework',
  themeConfig: {
    repo: 'https://github.com/tspayne87/blue-jay',
    editLinks: false,
    docsDir: '',
    editLinkText: '',
    lastUpdated: false,
    nav: [
      { text: 'Getting Started', link: '/guide/getting-started', activeMatch: '^/guide/' },
      { text: 'API', link: '/api/component-system-game', activeMatch: '^/api/'}
    ],
    sidebar: {
      '/guide/': [
        {
          text: 'Introduction',
          children: [
            { text: 'Getting Started', link: '/guide/getting-started' },
            { text: 'Views', link: '/guide/views' },
            { text: 'Addons', link: '/guide/addons' },
            { text: 'Systems', link: '/guide/systems' },
            { text: 'Entities', link: '/guide/entities' },
            { text: 'Queries', link: '/guide/queries' }
          ]
        }
      ],
      '/api/': [
        {
          text: 'BlueJay',
          children: [
            { text: 'Component System Game', link: '/api/component-system-game' },
            { text: 'Service Provider', link: '/api/service-provider' },
            { text: 'View Collection', link: '/api/view-collection' },
            { text: 'View', link: '/api/view' }
          ]
        },
        {
          text: 'BlueJay.Component.System',
          children: [
            { text: 'Addon', link: '/api/component-system/addon' },
            { text: 'Entity Collection', link: '/api/component-system/entity-collection' },
            { text: 'Entity', link: '/api/component-system/entity' },
            { text: 'Font Collection', link: '/api/component-system/font-collection' },
            { text: 'Key Helper', link: '/api/component-system/key-helper' },
            { text: 'Layer Collection', link: '/api/component-system/layer-collection' },
            { text: 'Layer', link: '/api/component-system/layer' },
            { text: 'Service Collection', link: '/api/component-system/service-collection' },
            { text: 'Service Provider', link: '/api/component-system/service-provider' },
            { text: 'System', link: '/api/component-system/system' }
          ]
        },
        {
          text: 'BlueJay.Core',
          children: [
            { text: 'Delta Service', link: '/api/core/delta-service' },
            { text: 'Graphics Device', link: '/api/core/graphics-device' },
            { text: 'Math Helper', link: '/api/core/math-helper' },
            { text: 'Nine Patch', link: '/api/core/nine-patch' },
            { text: 'Random', link: '/api/core/random' },
            { text: 'Rectangle Helper', link: '/api/core/rectangle-helper' },
            { text: 'Rectangle', link: '/api/core/rectangle' },
            { text: 'Size', link: '/api/core/size' },
            { text: 'Sprite Batch Extension', link: '/api/core/sprite-batch-extension' },
            { text: 'Sprite Batch', link: '/api/core/sprite-batch' },
            { text: 'Texture Font', link: '/api/core/texture-font' }
          ]
        },
        {
          text: 'BlueJay.Events',
          children: [
            { text: 'Service Provider', link: '/api/events/service-provider' },
            { text: 'Service Collection', link: '/api/events/service-collection' },
            { text: 'Event Queue', link: '/api/events/event-queue' },
            { text: 'Event Listener', link: '/api/events/event-listener' },
            { text: 'Event', link: '/api/events/event' },
            { text: 'Life Cycle Events', link: '/api/events/life-cycle-events' }
          ]
        },
        {
          text: 'BlueJay.Common',
          children: [
            { text: 'Introduction', link: '/api/common/introduction' },
            { text: 'Addons', link: '/api/common/addons' },
            { text: 'Events', link: '/api/common/events' },
            { text: 'Systems', link: '/api/common/systems' },
          ]
        }
      ]
    }
  }
}

export default config;