
const AppToast = (function () {

    const config = {
        duration: 3000,
        gravity: "bottom",
        position: "right",
        close: true,
        stopOnFocus: true
    };

    const styles = {
        success: {
            bg: "linear-gradient(135deg,#28a745,#5ddf80)",
            icon: "✅",
            className: "toast-slide"
        },
        error: {
            bg: "linear-gradient(135deg,#dc3545,#ff6b6b)",
            icon: "❌",
            className: "toast-shake"
        },
        warning: {
            bg: "linear-gradient(135deg,#ffc107,#ffdd57)",
            icon: "⚠️",
            className: "toast-slide"
        },
        info: {
            bg: "linear-gradient(135deg,#0dcaf0,#6edff6)",
            icon: "ℹ️",
            className: "toast-fade"
        }
    };

    function show(message, type = "success", options = {}) {

        const theme = styles[type] || styles.success;

        Toastify({
            text: `${theme.icon} ${message}`,
            duration: options.duration || config.duration,
            gravity: options.gravity || config.gravity,
            position: options.position || config.position,
            close: options.close ?? config.close,
            stopOnFocus: options.stopOnFocus ?? config.stopOnFocus,
            className: `toast-base ${theme.className}`,
            style: {
                background: theme.bg,
                borderRadius: "14px",
                boxShadow: "0 10px 30px rgba(0,0,0,.25)",
                fontSize: "14px",
                padding: "14px 18px"
            }
        }).showToast();
    }

    return {
        success: (msg, opt) => show(msg, "success", opt),
        error: (msg, opt) => show(msg, "error", opt),
        warning: (msg, opt) => show(msg, "warning", opt),
        info: (msg, opt) => show(msg, "info", opt)
    };

})();
