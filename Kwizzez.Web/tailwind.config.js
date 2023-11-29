/** @type {import('tailwindcss').Config} */
const colors = require("tailwindcss/colors");

module.exports = {
  content: ["./src/**/*.{html,ts}", "./node_modules/flowbite/**/*.js"],
  theme: {
    extend: {},
    colors: {
      primary: {
        light: colors.blue["300"],
        DEFAULT: colors.blue["500"],
        dark: colors.blue["600"],
      },
      success: {
        light: colors.emerald["300"],
        DEFAULT: colors.emerald["500"],
        dark: colors.emerald["600"],
      },
      secondary: {
        light: colors.slate["200"],
        DEFAULT: colors.slate["400"],
        dark: colors.slate["600"],
      },
      danger: {
        light: colors.red["200"],
        DEFAULT: colors.red["500"],
        dark: colors.red["600"],
      },
      dark: colors.slate["400"],
      black: colors.black,
      white: colors.white,
    },
  },
  plugins: [require("flowbite/plugin")],
};
