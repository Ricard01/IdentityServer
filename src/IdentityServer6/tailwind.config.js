/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.chstml'
    ],
    theme: {

        extend: {
            colors: {
                darkpurple: {
                    50: '#e8e0ea',
                    100: '#c5b3ca',
                    200: '#9f80a6',
                    300: '#794d82',
                    400: '#5c2668',
                    500: '#3f004d',
                    600: '#390046',
                    700: '#31003d',
                    800: '#290034',
                    900: '#1b0025',
                },
            }
        },
  },
  plugins: [],
}
