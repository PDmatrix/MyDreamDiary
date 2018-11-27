export default {
  plugins: [
    ['umi-plugin-react', {
      antd: true,
      dva: false,
      dynamicImport: true,
      title: 'Дневник снов',
      dll: false,
      routes: {
        exclude: [],
      },
      hardSource: true,
    }],
  ],
}
