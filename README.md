This is a prototype with the core gameplay of https://play.google.com/store/apps/details?id=com.aa.puffup&hl=en. Prototype was created as as an exercise and thus has very limited graphics, no sounds, no side-features but it's a good showcase of how to implement a simple but scalable game structure.

Code features:
- Abstract level & sublevels with custom implementations for each one
- Factory for obstacles & levels
- Interfaces for items that require runtime initialization
- Easy to extend base items (obstacles, enemies, borders)
- Event actions for most items interactions

Scalability features:
- Scriptables for each item for easy customization
- Prefabs + variants for each item & UI
- Main scene does not require changes when new levels are added

Good to have:
- Unique component for all texts that require animations
- Prints enabeld/disbaled by a property
- Plugin for no effort animations
- Plugin for properly handling vibrations (durations, patterns)

ToDos:
- Implement sound system
- Implement minimal save system for level/sublevel
- Update UI & input to use newer systems
- Balance game so it feels more unique
- Create more levels, sublevels, custom bubbles/enemies
- Implement new game mechanics
