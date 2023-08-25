export const treeData = [
  {
    path: 'user',
    icon: 'user',
    name: 'user',
  },
  {
    path: 'home',
    icon: 'home',
    name: 'home'
  },
  {
    name: 'PR section',
    children: [
      {
        name: 'pr',
        icon: 'dumbbell',
        path: 'pr'
      }
    ]
  },
  {
    name: 'Workout Plans',
    children: [
      {name: 'workouts', icon: 'calendar', path: 'workouts/home'},
      {name: 'add workout', icon: 'pen', path: 'workouts/add/-1'}
    ]
  },
]
